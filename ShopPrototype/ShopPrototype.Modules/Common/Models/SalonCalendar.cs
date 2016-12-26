using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopPrototype.Modules.Common.Models
{
	public class SalonCalendar
	{
		public int SalonId { get; set; }

		public string SalonName { get; set; }

		public DateTime ScheduleDate { get; set; }

		public IEnumerable<CalendarFacilityItem> Facilities { get; set; }
	}

	public class CalendarFacilityItem
	{
		public int FacilityId { get; set; }

		public string FacilityName { get; set; }

		public IEnumerable<FacilityTimeSlotItem> TimeSlots { get; set; }
	}

	public class FacilityTimeSlotItem
	{
		public Guid Id { get; set; }

		public DateTime SlotStartsAt { get; set; }

		public DateTime SlotEndsAt { get; set; }

		public bool Available { get; set; }

		public string SlotTime
		{
			get
			{
				return string.Format("{0} - {1}", SlotStartsAt.ToShortTimeString(), SlotEndsAt.ToShortTimeString());
			}
		}
	}
}
