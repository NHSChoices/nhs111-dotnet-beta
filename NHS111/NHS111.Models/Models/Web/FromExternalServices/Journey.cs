using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace NHS111.Models.Models.Web.FromExternalServices
{
    public class Journey
    {
        [JsonProperty(PropertyName = "steps")]
        public List<JourneyStep> Steps { get; set; }

        public Journey()
        {
            Steps = new List<JourneyStep>();
        }

        public Journey Add(Journey otherJourney)
        {
            otherJourney.Steps.ForEach(step => Steps.Add(step));
            return this;
        }
    }
}