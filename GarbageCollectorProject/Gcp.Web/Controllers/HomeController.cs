using System.Threading.Tasks;
using System.Web.Mvc;

namespace Gcp.Web.Controllers
{
	[Authorize]
	public class HomeController : Controller
    {
		// GET: Home
		[Authorize(Roles = "admin")]
		public ActionResult Index()
        {
            return View();
        }
    }
}