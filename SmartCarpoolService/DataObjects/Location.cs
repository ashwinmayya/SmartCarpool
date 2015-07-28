using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Mobile.Service;

namespace SmartCarpoolServiceService.DataObjects
{
	public class Location : EntityData
	{
		public string Name { get; set; }
		public double Lat { get; set; }
		public double Lon { get; set; }

	}
}
