using NHS111.Models.IoC;
using NHS111.Utils.Cache;
using NHS111.Utils.IoC;
using NHS111.Utils.Notifier;
using NHS111.Web.Presentation.IoC;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace NHS111.Web.IoC
{
    public class WebRegistry : Registry
    {
        public WebRegistry()
        {
            IncludeRegistry<UtilsRegistry>();
            IncludeRegistry<ModelsRegistry>();
            IncludeRegistry<WebPresentationRegistry>();
            For<ICacheManager<string, string>>().Use(new RedisManager("111alpha.redis.cache.windows.net:6380,abortConnect=false,ssl=true,password=Z9AxD1Pb8KmVxsCivQn4teSVmkg272zXDBXE3Z4L/QY="));
            For<INotifier<string>>().Use<Notifier>();

            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
            });
        }

    }
}