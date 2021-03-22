using System.Web.Mvc;
using System.Web.Routing;
using TB.Kutuphane.WebUI.Tasks.Triggers;

namespace TB.Kutuphane.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            CezaArtirmaAzaltmaTrigger.Baslat();
        }
    }
}
