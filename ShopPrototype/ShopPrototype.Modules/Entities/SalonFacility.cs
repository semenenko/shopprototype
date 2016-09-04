namespace ShopPrototype.Modules.Entities
{
	public class SalonFacility
	{
		public int SalonId { get; set; }

		public int FacilityId { get; set; }

		public int? DurationMin { get; set; }

		public virtual Facility Facility { get; set; }

		public virtual Salon Salon { get; set; }
	}
}
