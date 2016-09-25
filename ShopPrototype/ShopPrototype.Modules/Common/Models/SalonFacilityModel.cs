namespace ShopPrototype.Modules.Common.Models
{
	public class SalonFacilityModel
	{
		public int FacilityId { get; set; }

		public int FacilityCategoryId { get; set; }

		public string FacilityTitle { get; set; }

		public string CategoryTitle { get; set; }

		public int? DurationMin { get; set; }

		public bool Selected { get; set; }
	}
}
