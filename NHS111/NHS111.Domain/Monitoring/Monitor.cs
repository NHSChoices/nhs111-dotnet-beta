using System;
using System.Threading.Tasks;
using NHS111.Domain.Repository;
using NHS111.Utils.Monitoring;

namespace NHS111.Domain.Monitoring
{
    public class Monitor : BaseMonitor
    {
        private readonly IMonitorRepository _monitorRepository;

        public Monitor(IMonitorRepository monitorRepository)
        {
            _monitorRepository = monitorRepository;
        }

        public override string Metrics()
        {
            return "Metrics";
        }

        public override async Task<bool> Health()
        {
            try
            {
                return await _monitorRepository.CheckHealth();
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }
    }
}