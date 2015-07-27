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
    public class RideController : TableController<Ride>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            SmartCarpoolServiceContext context = new SmartCarpoolServiceContext();
            DomainManager = new EntityDomainManager<Ride>(context, Request, Services);
        }

        // GET tables/Ride
        public IQueryable<Ride> GetAllRide()
        {
            return Query(); 
        }

        // GET tables/Ride/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Ride> GetRide(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Ride/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Ride> PatchRide(string id, Delta<Ride> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Ride/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public async Task<IHttpActionResult> PostRide(Ride item)
        {
            Ride current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Ride/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteRide(string id)
        {
             return DeleteAsync(id);
        }

    }
}