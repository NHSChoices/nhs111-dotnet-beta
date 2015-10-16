﻿using System.Configuration;

namespace NHS111.Web.Presentation.Configuration
{
    public class Configuration : IConfiguration
    {
        public string GPSearchUrl { get { return ConfigurationManager.AppSettings["GPSearchUrl"]; } }
        public string GPSearchApiUrl { get { return ConfigurationManager.AppSettings["GPSearchApiUrl"]; } }
        public string GPSearchByIdUrl { get { return ConfigurationManager.AppSettings["GPSearchByIdUrl"]; } }
        public string BusinessApiPathwayUrl { get { return ConfigurationManager.AppSettings["BusinessApiPathwayUrl"]; } }
        public string BusinessApiGroupedPathwaysUrl { get { return ConfigurationManager.AppSettings["BusinessApiGroupedPathwaysUrl"]; } }
        public string BusinessDosCheckCapacitySummaryUrl { get { return ConfigurationManager.AppSettings["BusinessDosCheckCapacitySummaryUrl"]; } }
        public string BusinessDosServiceDetailsByIdUrl { get { return ConfigurationManager.AppSettings["BusinessDosServiceDetailsByIdUrl"]; } }
        public string BusinessApiJustToBeSafePartOneUrl { get { return ConfigurationManager.AppSettings["BusinessApiJustToBeSafePartOneUrl"]; } }
        public string BusinessApiJustToBeSafePartTwoUrl { get { return ConfigurationManager.AppSettings["BusinessApiJustToBeSafePartTwoUrl"]; } }
        public string PostcodeSearchByIdApiUrl { get { return ConfigurationManager.AppSettings["PostcodeSearchByIdApiUrl"]; } }
        public string PostcodeSubscriptionKey { get { return ConfigurationManager.AppSettings["PostcodeSubscriptionKey"]; } }
        public string BusinessApiPathwayIdUrl { get { return ConfigurationManager.AppSettings["BusinessApiPathwayIdUrl"]; } }
        public string BusinessApiPathwaySymptomGroupUrl { get { return ConfigurationManager.AppSettings["BusinessApiPathwaySymptomGroupUrl"]; } }
        public string BusinessApiNextNodeUrl { get { return ConfigurationManager.AppSettings["BusinessApiNextNodeUrl"]; } }
        public string BusinessApiQuestionByIdUrl { get { return ConfigurationManager.AppSettings["BusinessApiQuestionByIdUrl"]; } }
        public string BusinessApiCareAdviceUrl { get { return ConfigurationManager.AppSettings["BusinessApiCareAdviceUrl"]; } }
        public string IntegrationApiItkDispatcher { get { return ConfigurationManager.AppSettings["IntegrationApiItkDispatcher"]; } }
        public string BusinessApiFirstQuestionUrl { get { return ConfigurationManager.AppSettings["BusinessApiFirstQuestionUrl"]; } }
        public string RedisConnectionString { get { return ConfigurationManager.AppSettings["RedisConnectionString"]; } }
    }

    public interface IConfiguration
    {
        string GPSearchUrl { get; }
        string GPSearchApiUrl { get; }
        string GPSearchByIdUrl { get; }
        string BusinessApiPathwayUrl { get; }
        string BusinessApiGroupedPathwaysUrl { get; }
        string BusinessDosCheckCapacitySummaryUrl { get; }
        string BusinessDosServiceDetailsByIdUrl { get; }
        string BusinessApiJustToBeSafePartOneUrl { get; }
        string BusinessApiJustToBeSafePartTwoUrl { get; }
        string PostcodeSearchByIdApiUrl { get; }
        string PostcodeSubscriptionKey { get; }
        string BusinessApiPathwayIdUrl { get; }
        string BusinessApiPathwaySymptomGroupUrl { get; }
        string BusinessApiNextNodeUrl { get; }
        string BusinessApiQuestionByIdUrl { get; }
        string BusinessApiCareAdviceUrl { get; }
        string IntegrationApiItkDispatcher { get; }
        string BusinessApiFirstQuestionUrl { get; }
        string RedisConnectionString { get; }
    }
}