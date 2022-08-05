using Microsoft.AspNetCore.Mvc;

namespace Intranet.Controllers.Business
{
    public class OSRController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
