using System.Collections.Generic;

namespace ShopPrototype.Modules.Entities
{
	public class FacilityCategory
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public int SortOrder { get; set; }

		public ICollection<Facility> Facilities { get; set; }
	}
}
