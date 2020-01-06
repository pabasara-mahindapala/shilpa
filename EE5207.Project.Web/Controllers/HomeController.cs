using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace EE5207.Project.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : ProjectControllerBase
    {
        public ActionResult Index()
        {
            return View("~/App/Main/views/layout/layout.cshtml"); //Layout of the angular application.
        }
	}
}