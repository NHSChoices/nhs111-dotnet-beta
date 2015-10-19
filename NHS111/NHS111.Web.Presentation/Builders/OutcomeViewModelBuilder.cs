﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using Newtonsoft.Json;
using NHS111.Models.Models.Web;
using NHS111.Models.Models.Web.FromExternalServices;
using NHS111.Utils.Cache;
using NHS111.Utils.Helpers;
using NHS111.Utils.Itk;
using IConfiguration = NHS111.Web.Presentation.Configuration.IConfiguration;

namespace NHS111.Web.Presentation.Builders
{
    public class OutcomeViewModelBuilder : IOutcomeViewModelBuilder
    {
        private readonly IDOSBuilder _dosBuilder;
        private readonly ICareAdviceBuilder _careAdviceBuilder;
        private readonly IRestfulHelper _restfulHelper;
        private readonly IConfiguration _configuration;
        private readonly IMappingEngine _mappingEngine;
        private readonly ICacheManager<string, string> _cacheManager;

        public OutcomeViewModelBuilder(ICareAdviceBuilder careAdviceBuilder, IRestfulHelper restfulHelper, IConfiguration configuration, IMappingEngine mappingEngine, ICacheManager<string, string> cacheManager)
        {
            _careAdviceBuilder = careAdviceBuilder;
            _restfulHelper = restfulHelper;
            _configuration = configuration;
            _mappingEngine = mappingEngine;
            _cacheManager = cacheManager;
        }

        public async Task<OutcomeViewModel> SearchSurgeryBuilder(string input)
        {
            var model = new OutcomeViewModel();
            if (!string.IsNullOrEmpty(input))
            {
                var surgeriers = JsonConvert.DeserializeObject<List<Surgery>>(await _restfulHelper.GetAsync(string.Format(_configuration.GPSearchByIdUrl, input)));
                model.SurgeryViewModel.Surgeries = surgeriers;
            }

            return model;
        }

        public async Task<List<AddressInfo>> SearchPostcodeBuilder(string input)
        {
            input = HttpUtility.UrlDecode(input);
            var listPaf = JsonConvert.DeserializeObject<List<PAF>>(await _restfulHelper.GetAsync(string.Format(_configuration.PostcodeSearchByIdApiUrl, input)));
            return _mappingEngine.Map<List<AddressInfo>>(listPaf);
        }

        public async Task<OutcomeViewModel> DispositionBuilder(OutcomeViewModel model)
        {
            //TODO -- DUMMY DATA, REMOVE
            var user = Users.GetRandomUser();
            var years = (DateTime.Now - user.DoB).Days / 365;
            model.UserInfo.Age = years;
            model.UserInfo.Gender = user.Gender;
            //-----------------------------

            model.UserId = Guid.NewGuid();
            var journey = JsonConvert.DeserializeObject<Journey>(model.JourneyJson);
            var itkMessage = new ItkMessageBuilder(_cacheManager).WithExample().SetSummaryItems(
                journey.Steps.Select(a => new ItkMessageBuilder.SummaryItem(a.QuestionNo, a.QuestionTitle, a.Answer.Title))
                )
                .SetGender(model.UserInfo.Gender)
                .SetDateOfBirth(DateTime.Now.AddYears(-model.UserInfo.Age).ToShortDateString())
                .SetDispositionCode(model.Id.Replace("Dx", string.Empty))
                .SetProvider("111Digital")
                .SetInformantType("NotSpecified")
                .SetSendToRepeatCaller(false)
                .SetForename(user.GivenName)
                .SetSurname(user.FamilyName)
                .SetDateOfBirth(user.DoB.ToString("yyyy-MM-dd"))
                .SetGender(user.Gender)
                .SetHomeAddress(new ItkMessageBuilder.Address(string.Format("{0} {1} {2}", user.AddressLine1, user.AddressLine2, user.AddressLine3),
                    string.Format("{0} {1}", user.AddressLine4, user.AddressLine5),
                    user.Postcode))
                .Build(model.UserId.ToString());

            model.CareAdvices = await _careAdviceBuilder.FillCareAdviceBuilder(model.UserInfo.Age, model.UserInfo.Gender, model.CareAdviceMarkers.ToList());
            model.SymptomGroup = await _restfulHelper.GetAsync(string.Format(_configuration.BusinessApiPathwaySymptomGroupUrl,
                string.Join(",", journey.Steps.Select(s => s.QuestionId.Split('.').First()).Distinct())));

            return model;
        }

        public async Task<OutcomeViewModel> PostCodeSearchBuilder(OutcomeViewModel model)
        {
            var addresses = await SearchPostcodeBuilder(model.AddressSearchViewModel.PostCode);
            model.AddressSearchViewModel.AddressInfoList = addresses;
            model.AddressSearchViewModel.PostcodeApiAddress = _configuration.PostcodeSearchByIdApiUrl;
            model.AddressSearchViewModel.PostcodeApiSubscriptionKey = _configuration.PostcodeSubscriptionKey;
            return model;
        }

        public async Task<OutcomeViewModel> PersonalDetailsBuilder(OutcomeViewModel model)
        {
            if (!string.IsNullOrEmpty(model.AddressSearchViewModel.PostCode))
            {
                return await PostCodeSearchBuilder(model);
            }

            model.AddressSearchViewModel.PostcodeApiAddress = _configuration.PostcodeSearchByIdApiUrl;
            model.AddressSearchViewModel.PostcodeApiSubscriptionKey = _configuration.PostcodeSubscriptionKey;

            model.CareAdvices = await _careAdviceBuilder.FillCareAdviceBuilder(model.UserInfo.Age, model.UserInfo.Gender, model.CareAdviceMarkers.ToList());
            return model;
        }

    }

    public interface IOutcomeViewModelBuilder
    {
        Task<OutcomeViewModel> SearchSurgeryBuilder(string input);
        Task<List<AddressInfo>> SearchPostcodeBuilder(string input);
        Task<OutcomeViewModel> DispositionBuilder(OutcomeViewModel model);
        Task<OutcomeViewModel> PostCodeSearchBuilder(OutcomeViewModel model);
        Task<OutcomeViewModel> PersonalDetailsBuilder(OutcomeViewModel model);
    }
}