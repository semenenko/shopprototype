namespace ShopPrototype.Modules.Admin.Models
{
	public class FacilityModel
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public int DurationMin { get; set; }

		public int SortOrder { get; set; }

		public int FacilityCategoryId { get; set; }
	}
}
