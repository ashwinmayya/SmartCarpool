using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Notifications;
using Microsoft.WindowsAzure.Mobile.Service;

namespace SmartCarpoolServiceService.DataObjects
{
	public abstract class RideRequest : EntityData
	{
		public string StartLocationId { get; set; }

		public string EndLocationId { get; set; }

		public DateTime StartTime { get; set; }

		public int Points { get; set; }

		public RideRequestStatus RideRequestStatus { get; set; }

		public string UserId { get; set; }

		[ForeignKey("UserId")]
		public User User { get; set; }

		[ForeignKey("StartLocationId")]
		public Location StartLocation { get; set; }

		[ForeignKey("EndLocationId")]
		public Location EndLocation { get; set; }
	}

	public class DriverRequest : RideRequest
	{
		public string RideId { get; set; }

		[ForeignKey("RideId")]
		public Ride Ride { get; set; }
	}

	public class PassengerRequest : RideRequest
	{
		public string RideId { get; set; }

		[ForeignKey("RideId")]
		public Ride Ride { get; set; }
	}

	public enum RideRequestStatus
	{
		Waiting,

		InProgress,

		Completed
	}
}
