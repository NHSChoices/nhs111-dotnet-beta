using System.Web.Mvc;
using NHS111.Utils.Logging;

namespace NHS111.Utils.Attributes
{
    public class LogHandleErrorForMVCAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            var controllerAction = string.Empty;
            if (filterContext.RouteData != null && filterContext.RouteData != null & filterContext.RouteData.Values != null)
            {
                controllerAction = string.Join("/", filterContext.RouteData.Values.Values);
            }

            Log4Net.Error(string.Format("ERROR on {0}:  {1} - {2} - {3}", controllerAction, filterContext.Exception.Message, filterContext.Exception.StackTrace, filterContext.Exception.Data));
        }
    }
}
