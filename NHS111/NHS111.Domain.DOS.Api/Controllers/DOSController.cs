﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using NHS111.Domain.DOS.Api.Configuration;
using NHS111.Utils.Attributes;
using NHS111.Utils.Helpers;

namespace NHS111.Domain.DOS.Api.Controllers
{
    [LogHandleErrorForApi]
    public class DOSController : ApiController
    {
        private readonly IRestfulHelper _restfulHelper;
        private readonly IConfiguration _configuration;

        public DOSController(IRestfulHelper restfulHelper, IConfiguration configuration)
        {
            _restfulHelper = restfulHelper;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("DOSapi/CheckCapacitySummary")]
        public async Task<HttpResponseMessage> CheckCapacitySummary(HttpRequestMessage request)
        {
            return await _restfulHelper.PostAsync(_configuration.DOSIntegrationCheckCapacitySummaryUrl, request);
        }

        [HttpPost]
        [Route("DOSapi/ServiceDetailsById")]
        public async Task<HttpResponseMessage> ServiceDetailsById(HttpRequestMessage request)
        {
            return await _restfulHelper.PostAsync(_configuration.DOSIntegrationServiceDetailsByIdUrl, request);
        }
    }
}
