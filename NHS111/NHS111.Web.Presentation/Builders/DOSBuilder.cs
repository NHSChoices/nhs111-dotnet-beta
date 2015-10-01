using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;
using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NHS111.Models.Models.Web;
using NHS111.Models.Models.Web.FromExternalServices;
using NHS111.Utils.Cache;
using NHS111.Utils.Helpers;
using NHS111.Utils.Notifier;
using IConfiguration = NHS111.Web.Presentation.Configuration.IConfiguration;

namespace NHS111.Web.Presentation.Builders
{
    public class DOSBuilder : IDOSBuilder
    {
        private readonly ICareAdviceBuilder _careAdviceBuilder;
        private readonly IRestfulHelper _restfulHelper;
        private readonly IConfiguration _configuration;
        private readonly IMappingEngine _mappingEngine;
        private readonly ICacheManager<string, string> _cacheManager;
        private readonly INotifier<string> _notifier;

        public DOSBuilder(ICareAdviceBuilder careAdviceBuilder, IRestfulHelper restfulHelper, IConfiguration configuration, IMappingEngine mappingEngine, ICacheManager<string, string> cacheManager, INotifier<string> notifier)
        {
            _careAdviceBuilder = careAdviceBuilder;
            _restfulHelper = restfulHelper;
            _configuration = configuration;
            _mappingEngine = mappingEngine;
            _cacheManager = cacheManager;
            _notifier = notifier;
        }

        public async Task<DosViewModel> DosResultsBuilder(OutcomeViewModel outcomeViewModel)
        {
            var model = _mappingEngine.Map<DosViewModel>(outcomeViewModel);
            var surgery = new Surgery();
            if (!string.IsNullOrEmpty(model.SelectedSurgery))
                surgery = JsonConvert.DeserializeObject<Surgery>(await _restfulHelper.GetAsync(string.Format(_configuration.GPSearchApiUrl, model.SelectedSurgery)));
            else
                surgery.SurgeryId = "UKN";

            var data = new StringBuilder("{\"serviceVersion\":\"1.3\",\"userInfo\":{\"username\":\"digital111_ws\",\"password\":\"Valtech111\"},")
            .Append("\"c\":{\"caseRef\":\"123\",\"caseId\":\"123\",\"postcode\":\"" + model.PostCode + "\",\"surgery\":\"")
            .Append(surgery.SurgeryId + "\",\"age\":" + model.Age + ",")
            .Append("\"ageFormat\":0,\"disposition\":" + model.Id.Replace("Dx", "10"))
            .Append(",\"symptomGroup\":" + model.SymptomGroup + ",\"symptomDiscriminatorList\":[" + model.SymptomDiscriminator + "],")
            .Append("\"searchDistanceSpecified\":false,\"gender\":\"" + model.Gender.First() + "\"}}");

            var request = new HttpRequestMessage { Content = new StringContent(data.ToString(), Encoding.UTF8, "application/json") };
            var response = await _restfulHelper.PostAsync(_configuration.BusinessDosCheckCapacitySummaryUrl, request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var val = await response.Content.ReadAsStringAsync();
                model.CheckCapacitySummaryResultListJson = HttpUtility.HtmlDecode(val);
                var jObj = (JObject)JsonConvert.DeserializeObject(val);
                var result = jObj["CheckCapacitySummaryResult"];
                model.CheckCapacitySummaryResultList = result.ToObject<CheckCapacitySummaryResult[]>();
            }
            else
            {
                model.CheckCapacitySummaryResultList = new CheckCapacitySummaryResult[0];
            }

            return model;
        }

        public async Task<DosViewModel> FillServiceDetailsBuilder(DosViewModel model)
        {
            var jObj = (JObject)JsonConvert.DeserializeObject(model.CheckCapacitySummaryResultListJson);
            model.CheckCapacitySummaryResultList = jObj["CheckCapacitySummaryResult"].ToObject<CheckCapacitySummaryResult[]>();
            var selectedService = model.CheckCapacitySummaryResultList.FirstOrDefault(x => x.IdField == Convert.ToInt32(model.SelectedService));

            var itkMessage = await _cacheManager.Read(model.UserId.ToString());
            var document = XDocument.Parse(itkMessage);

            var serviceDetials = document.Root.Descendants("ServiceDetails").FirstOrDefault();
            serviceDetials.Element("id").SetValue(selectedService.IdField.ToString());
            serviceDetials.Element("name").SetValue(selectedService.NameField.ToString());
            serviceDetials.Element("odsCode").SetValue(selectedService.OdsCodeField.ToString());
            serviceDetials.Element("contactDetails").SetValue(selectedService.ContactDetailsField ?? "");
            serviceDetials.Element("address").SetValue(selectedService.AddressField.ToString());
            serviceDetials.Element("postcode").SetValue(selectedService.PostcodeField.ToString());

            _cacheManager.Set(model.UserId.ToString(), document.ToString());
            _notifier.Notify(_configuration.IntegrationApiItkDispatcher, model.UserId.ToString());

            model.CheckCapacitySummaryResultList = new CheckCapacitySummaryResult[] { selectedService };
            model.CareAdvices = await _careAdviceBuilder.FillCareAdviceBuilder(Int32.Parse(model.Age), model.Gender, model.CareAdviceMarkers.ToList());

            return model;
        }
    }

    public interface IDOSBuilder
    {
        Task<DosViewModel> DosResultsBuilder(OutcomeViewModel outcomeViewModel);
        Task<DosViewModel> FillServiceDetailsBuilder(DosViewModel model);
    }
}