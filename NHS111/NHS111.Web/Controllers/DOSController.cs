using System.Threading.Tasks;
using System.Web.Mvc;
using NHS111.Models.Models.Web;
using NHS111.Utils.Attributes;
using NHS111.Web.Presentation.Builders;

namespace NHS111.Web.Controllers
{
    [LogHandleErrorForMVC]
    public class DOSController : Controller
    {
        private readonly IDOSBuilder _dosBuilder;

        public DOSController(IDOSBuilder dosBuilder)
        {
            _dosBuilder = dosBuilder;
        }

        //[HttpPost] //TODO delete
        //public async Task<ActionResult> DosResults(OutcomeViewModel model)
        //{
        //    var viewModel = await _dosBuilder.DosResultsBuilder(model);
        //    return View("DosResults", viewModel);
        //}

        [HttpPost]
        public async Task<ActionResult> FillServiceDetails(DosViewModel model)
        {
            return View("../DOS/Confirmation", await _dosBuilder.FillServiceDetailsBuilder(model));
        }
    }
}