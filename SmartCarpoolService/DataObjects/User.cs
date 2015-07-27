using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.WindowsAzure.Mobile.Service.Tables;

namespace SmartCarpoolServiceService.DataObjects
{
	public class User : EntityData
	{
		public string Name { get; set; }

		public string AuthId { get; set; }

		public int TotalPoints { get; set; }
	}
}

