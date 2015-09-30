using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NHS111.Models.Models.Business;
using NHS111.Models.Models.Domain;
using NHS111.Utils.Comparer;

namespace NHS111.Business.Services
{
    public class SearchCorrectionService : ISearchCorrectionService
    {
        private readonly IPathwayService _pathwayService;

        public SearchCorrectionService(IPathwayService pathwayService)
        {
            _pathwayService = pathwayService;
        }


        //TODO  remove commented out code
        //private static readonly List<Suggestion> Suggestions = new List<Suggestion>
        //{
        //    new Suggestion
        //    {
        //        CorrectTerm = "Headache",
        //        MispelledTerm =
        //            new List<string>(new[]
        //            {
        //                "migraine headache", "migratin headache", "my brain headache", "migrane", "head pain",
        //                "pain in the head", "sore head", "bad head", "hedache", "head ache", "hedayk", "hedake",
        //                "headayk", "hedayk", "hed", "head", "terrible headache", "throbbing headache", "stress headache",
        //                "brain", "hurt head", "tension headache", "tension", "cluster headache", "blinding headache"
        //            })
        //    },
        //    new Suggestion
        //    {
        //        CorrectTerm = "Head injury, penetrating",
        //        MispelledTerm =
        //            new List<string>(new[]
        //            {
        //                "Banged head", "bumped head", "head sore", "sore head", "thump head", "hit head", "bruised head",
        //                "cut head", "lacerated head", "head wound", "wounded head", "woonded head", "concussion",
        //                "concushion", "scalp wound", "scalp", "cut head", "head trauma", "haematoma", "hematoma",
        //                "skull fracture", "skull", "contusion", "brain", "hed", "cerebral", "injury"
        //            })
        //    },
        //    new Suggestion
        //    {
        //        CorrectTerm = "Head injury, blunt",
        //        MispelledTerm =
        //            new List<string>(new[]
        //            {
        //                "Banged head", "bumped head", "head sore", "sore head", "thump head", "hit head", "bruised head",
        //                "cut head", "head wound", "wounded head", "woonded head", "concussion", "concushion",
        //                "scalp wound", "scalp", "cut head", "head trauma", "haematoma", "hematoma", "skull fracture",
        //                "skull", "contusion", "brain", "hed", "cerebral", "injury", "blunt", "", "swollen head"
        //            })
        //    }
        //};

        public async Task<string> GetCorrection(string input)
        {
            input = input.ToLower();
            var pathways = JsonConvert.DeserializeObject<List<GroupedPathways>>(await _pathwayService.GetPathways(true));
            //var correctedSuggestions = Suggestions.Where(x => x.MispelledTerm.Any(y => y.ToLower().Contains(input))).Select(x => x.CorrectTerm);
            //var correctedTerms = new List<GroupedPathways>();
            //foreach (var correctedSuggestion in correctedSuggestions)
            //{
                //correctedTerms.Add(new GroupedPathways
                //{
                //    Group = correctedSuggestion,
                //    PathwayNumbers = pathways.Where(x => String.Equals(x.Group, correctedSuggestion, StringComparison.CurrentCultureIgnoreCase))
                //        .Select(x => x.PathwayNumbers)
                //        .FirstOrDefault()
                //});

            //}
            var pathwaysMatches = pathways.Where(x => x.Group.ToLower().Contains(input)).ToList();
            if (!pathwaysMatches.Any())// && !correctedTerms.Any())
                return JsonConvert.SerializeObject(pathways);

            //pathwaysMatches.AddRange(correctedTerms);
            return JsonConvert.SerializeObject(pathwaysMatches.Distinct(new PathwaysComparer()).OrderByDescending(x=>x.Group));
        }
    }

    public interface ISearchCorrectionService
    {
        Task<string> GetCorrection(string input);
    }
}