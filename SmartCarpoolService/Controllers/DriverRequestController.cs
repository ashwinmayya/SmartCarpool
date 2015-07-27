using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using SmartCarpoolServiceService.DataObjects;
using SmartCarpoolServiceService.Models;

namespace SmartCarpoolServiceService.Controllers
{
    public class DriverRequestController : TableController<DriverRequest>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            SmartCarpoolServiceContext context = new SmartCarpoolServiceContext();
            DomainManager = new EntityDomainManager<DriverRequest>(context, Request, Services);
        }

        // GET tables/DriverRequest
        public IQueryable<DriverRequest> GetAllDriverRequest()
        {
            return Query(); 
        }

        // GET tables/DriverRequest/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<DriverRequest> GetDriverRequest(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/DriverRequest/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<DriverRequest> PatchDriverRequest(string id, Delta<DriverRequest> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/DriverRequest/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public async Task<IHttpActionResult> PostDriverRequest(DriverRequest item)
        {
            DriverRequest current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/DriverRequest/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteDriverRequest(string id)
        {
             return DeleteAsync(id);
        }

    }
}