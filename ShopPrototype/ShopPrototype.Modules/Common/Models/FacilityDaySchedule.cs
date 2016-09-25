using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopPrototype.Modules.Common.Models
{
	public class FacilityDaySchedule
	{
		public int SalonId { get; set; }

		public int FacilityId { get; set; }

		public DateTime Date { get; set; }

		public IEnumerable<ScheduleItemModel> Items { get; set; }
	}

	public class ScheduleItemModel
	{
		public int SalonId { get; set; }

		public int FacilityId { get; set; }

		public bool Available { get; set; }

		public DateTime StartDateTime { get; set; }

		public int DurationMin { get; set; }
	}
}
