using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Mobile.Service;

namespace SmartCarpoolServiceService.DataObjects
{
	public class Ride : EntityData
	{
		public Ride()
		{
			PassengerRequests = new HashSet<PassengerRequest>();
		}

		public bool IsComplete { get; set; }

		public int TotalSeats { get; set; }

		public int AvailableSeats { get; set; }

		public DriverRequest DriverRequest { get; set; }

		public virtual ICollection<PassengerRequest> PassengerRequests { get; set; }
	}
}