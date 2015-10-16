using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using NHS111.Models.Models.Configuration;

namespace NHS111.Utils.Configuration
{
    public class PathwaysConfigurationManager : IPathwaysConfigurationManager
    {
        private static PathwaysSection GetPathwaysSection()
        {
            return ConfigurationManager.GetSection(PathwaysSection.SectionName) as PathwaysSection;
        }

        public static IEnumerable<LivePathwaysElement> GetLivePathwaysElements()
        {
            return from LivePathwaysElement p in GetPathwaysSection().LivePathways select p;
        }

        public bool UseLivePathways
        {
            get { return GetPathwaysSection().UseLivePathways;; }
        }
    }

    public interface IPathwaysConfigurationManager
    {
        bool UseLivePathways { get; }
    }
}
