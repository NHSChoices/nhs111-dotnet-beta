using System.Collections.Generic;
using System.Threading.Tasks;
using NHS111.Business.Configuration;
using NHS111.Utils.Helpers;

namespace NHS111.Business.Services
{
    public class CareAdviceService : ICareAdviceService
    {
        private readonly IConfiguration _configuration;
        private readonly IRestfulHelper _restfulHelper;

        public CareAdviceService(IConfiguration configuration, IRestfulHelper restfulHelper)
        {
            _configuration = configuration;
            _restfulHelper = restfulHelper;
        }

        public async Task<string> GetCareAdvice(int age, string gender, IEnumerable<string> markers)
        {
            return await _restfulHelper.GetAsync(_configuration.GetDomainApiCareAdviceUrl(age, gender, markers));
        }
    }

    public interface ICareAdviceService
    {
        Task<string> GetCareAdvice(int age, string gender, IEnumerable<string> markers);
    }
}