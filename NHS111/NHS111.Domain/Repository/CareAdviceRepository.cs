using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using NHS111.Models.Models.Domain;

namespace NHS111.Domain.Repository
{
    public class CareAdviceRepository : ICareAdviceRepository
    {
        private readonly IGraphRepository _graphRepository;

        public CareAdviceRepository(IGraphRepository graphRepository)
        {
            _graphRepository = graphRepository;
        }

        public async Task<IEnumerable<CareAdvice>> GetCareAdvice(int age, string gender, IEnumerable<string> markers) // TODO
        {
            string ageGroup = "";
            if (age >= 16) ageGroup = "Adult"; else if (5 <= age && age <= 15) ageGroup = "Child"; else if (1 <= age && age <= 4) ageGroup = "Toddler"; else ageGroup = "Infant";
            // TODO How to deal with infant vs. neonate?

            return await _graphRepository.Client.Cypher.
                Match("(c:CareAdviceText)").
                Where(string.Format("c.id in [{0}]", string.Join(",", markers.Select(marker => string.Format("\"{0}-{1}-{2}\"", marker, ageGroup, gender))))).
                Return(c => c.As<CareAdvice>()).
                ResultsAsync;
        }
    }

    public interface ICareAdviceRepository
    {
        Task<IEnumerable<CareAdvice>> GetCareAdvice(int age, string gender, IEnumerable<string> markers);
    }
}