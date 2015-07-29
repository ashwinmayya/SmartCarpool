using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using Newtonsoft.Json.Linq;
using SmartCarpoolServiceService.DataObjects;
using SmartCarpoolServiceService.Models;
using Util;

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

			var indexOperations = new List<Dictionary<string, object>>();
			
			// Start location
			JObject sLoc = new JObject();
			JArray sLonlat = new JArray();
			sLonlat.Add(current.StartLocation.Lon);
			sLonlat.Add(current.StartLocation.Lat);
			sLoc.Add("type", "Point");
			sLoc.Add("coordinates", sLonlat);
			Dictionary<string, object> dict = new Dictionary<string, object>();

			// End location
			JObject eLoc = new JObject();
			JArray eLonlat = new JArray();
			eLonlat.Add(current.StartLocation.Lon);
			eLonlat.Add(current.StartLocation.Lat);
			eLoc.Add("type", "Point");
			eLoc.Add("coordinates", eLonlat);

			dict.Add("RideRequestId", current.Id);
			dict.Add("StartLocationId", sLoc);
			dict.Add("EndLocation", eLoc);
			dict.Add("@search.action", "upload");

			//Id is required so I know which key to update in the search index
			//dict.Add("storeId", address.Id);
			//Rather than update, I will do a merge to update the new lat and lon
			//dict.Add("@search.action", "merge");

			indexOperations.Add(dict);

			AzureSearchHelper.IndexBatch(indexOperations);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/PassengerRequest/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeletePassengerRequest(string id)
        {
             return DeleteAsync(id);
        }

    }
}