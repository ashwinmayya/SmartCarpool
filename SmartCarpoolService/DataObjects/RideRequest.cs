using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Mobile.Service;

namespace SmartCarpoolServiceService.DataObjects
{
	public abstract class RideRequest : EntityData
	{
		public Location StartLocation { get; set; }

		public Location EndLocation { get; set; }

		public DateTime StartTime { get; set; }

		public int Points { get; set; }

		public RideRequestStatus RideRequestStatus { get; set; }

		public User User { get; set; }

	}

	public class DriverRequest : RideRequest
	{
	}

	public class PassengerRequest : RideRequest
	{
	}

	public enum RideRequestStatus
	{
		Waiting,

		InProgress,

		Completed
	}
}
