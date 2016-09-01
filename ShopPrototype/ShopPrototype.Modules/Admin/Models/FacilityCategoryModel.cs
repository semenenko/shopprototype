using System.Collections.Generic;

namespace ShopPrototype.Modules.Admin.Models
{
	public class FacilityCategoryModel
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public int SortOrder { get; set; }

		public IEnumerable<FacilityModel> Facilities { get; set; }
	}
}
