using System.Web.Mvc;

namespace TB.Kutuphane.WebUI.Controllers
{
    public class DefaultController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}