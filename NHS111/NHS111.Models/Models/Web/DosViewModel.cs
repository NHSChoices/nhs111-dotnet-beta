using System;
using System.Collections.Generic;
using NHS111.Models.Models.Domain;
using NHS111.Models.Models.Web.FromExternalServices;

namespace NHS111.Models.Models.Web
{
    public class DosViewModel
    {
        public Guid UserId { get; set; }
        public CheckCapacitySummaryResult[] CheckCapacitySummaryResultList { get; set; }
        public string CheckCapacitySummaryResultListJson { get; set; }
        public IEnumerable<CareAdvice> CareAdvices { get; set; }
        public IEnumerable<string> CareAdviceMarkers { get; set; }
        public string Title { get; set; }
        public string PostCode { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string Id { get; set; }
        public string SymptomGroup { get; set; }
        public string SymptomDiscriminator { get; set; }
        public string SelectedService { get; set; }
        public string JourneyJson { get; set; }
        public string PathwayNo { get; set; }
        public string SelectedSurgery { get; set; }

        public DosViewModel()
        {
            CareAdvices = new List<CareAdvice>();
            CareAdviceMarkers = new List<string>();
        }
    }
}