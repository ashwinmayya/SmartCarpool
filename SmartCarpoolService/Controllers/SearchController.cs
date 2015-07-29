using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SmartCarpoolServiceService.DataObjects;
using Util;

namespace SmartCarpoolServiceService.Controllers
{
    public class SearchController : ApiController
    {
	    public dynamic GetPossiblePassenger(double sLat, double sLon, double eLat, double eLon)
	    {

		    return AzureSearchHelper.Search(sLat, sLon, eLat, eLon);
	    }

    }
}
