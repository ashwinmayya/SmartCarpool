using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Mobile.Service;

namespace SmartCarpoolServiceService.DataObjects
{
	public class Ride : EntityData
	{
		public bool IsComplete { get; set; }

		public int TotalSeats { get; set; }

		public int AvailableSeats { get; set; }
	}
}