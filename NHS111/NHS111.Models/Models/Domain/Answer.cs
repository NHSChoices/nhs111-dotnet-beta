using System.Collections.Generic;
using Newtonsoft.Json;

namespace NHS111.Models.Models.Domain
{
    public class Answer
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "symptomDiscriminator")]
        public string SymptomDiscriminator { get; set; }

        [JsonProperty(PropertyName = "order")]
        public int Order { get; set; }

    }
}