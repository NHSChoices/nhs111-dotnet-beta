﻿using System.Configuration;

namespace NHS111.Domain.DOS.Api.Configuration
{
    public class Configuration : IConfiguration
    {
        public string DOSIntegrationBaseUrl { get { return ConfigurationManager.AppSettings["DOSIntegrationBaseUrl"]; } }

        public string DOSIntegrationCheckCapacitySummaryUrl {
            get
            {
                return string.Format("{0}{1}", ConfigurationManager.AppSettings["DOSIntegrationBaseUrl"],
                  ConfigurationManager.AppSettings["DOSIntegrationCheckCapacitySummaryUrl"]);

            }
        }

        public string DOSIntegrationServiceDetailsByIdUrl
        {
            get
            {
                return string.Format("{0}{1}", ConfigurationManager.AppSettings["DOSIntegrationBaseUrl"],
                  ConfigurationManager.AppSettings["DOSIntegrationServiceDetailsByIdUrl"]);

            }
        }

        public string DOSIntegrationMonitorHealthUrl
        {
            get
            {
                return string.Format("{0}{1}", ConfigurationManager.AppSettings["DOSIntegrationBaseUrl"],
                  ConfigurationManager.AppSettings["DOSIntegrationMonitorHealthUrl"]);

            }
        }
    }
}