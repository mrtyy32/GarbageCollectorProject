using System.Web.Http;
using Gcp.Host.Data;

namespace Gcp.Host.Controllers
{
    public class BaseController : ApiController
    {
        public GarbageCollectorsEntities db;
        public BaseController()
        {
            db = new GarbageCollectorsEntities();
			db.Configuration.LazyLoadingEnabled = false;
			db.Configuration.ProxyCreationEnabled = false;
		}
        protected override void Dispose(bool disposing)
        {
            db.Dispose();

        }
    }
}
