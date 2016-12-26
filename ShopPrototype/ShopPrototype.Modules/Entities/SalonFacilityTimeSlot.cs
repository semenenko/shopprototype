using System;

namespace ShopPrototype.Modules.Entities
{
	public class SalonFacilityTimeSlot
	{
		public int Id { get; set; }

		public int SalonId { get; set; }

		public int FacilityId { get; set; }

		public DateTime SlotStartsAt { get; set; }

		public DateTime SlotEndsAt { get; set; }

		public bool Available { get; set; }
	}
}
