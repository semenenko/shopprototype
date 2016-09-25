using System;
using System.Collections.Generic;

namespace ShopPrototype.Modules.Common.Models
{
	public class SalonSchedule
	{
		public int SalonId { get; set; }

		public DateTime Date { get; set; }

		public IEnumerable<FacilityDaySchedule> FacilitiesSchedules { get; set; }
	}
}
