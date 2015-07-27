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
    public class PassengerRequestController : TableController<PassengerRequest>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            SmartCarpoolServiceContext context = new SmartCarpoolServiceContext();
            DomainManager = new EntityDomainManager<PassengerRequest>(context, Request, Services);
        }

        // GET tables/PassengerRequest
        public IQueryable<PassengerRequest> GetAllPassengerRequest()
        {
            return Query(); 
        }

        // GET tables/PassengerRequest/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<PassengerRequest> GetPassengerRequest(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/PassengerRequest/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<PassengerRequest> PatchPassengerRequest(string id, Delta<PassengerRequest> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/PassengerRequest/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public async Task<IHttpActionResult> PostPassengerRequest(PassengerRequest item)
        {
            PassengerRequest current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/PassengerRequest/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeletePassengerRequest(string id)
        {
             return DeleteAsync(id);
        }

    }
}