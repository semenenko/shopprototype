using System.Collections.Generic;
using System.Linq;

namespace ShopPrototype.Modules.AdvancedSearch.Models
{
	public class SearchQuery
	{
		public string Lat { get; set; } 

		public string Long { get; set; }

		public IEnumerable<FacilitySearchItem> Facilities { get; set; }

		public IEnumerable<int> SelectedFacilitiesIds
		{
			get
			{
				return Facilities.Where(x => x.Selected).Select(x => x.Id).ToList();
			}
		}
	}

	public class FacilitySearchItem
	{
		public int Id { get; set; }

		public int FacilitySortOrder { get; set; }

		public int CategorySortOrder { get; set; }

		public string Title { get; set; }

		public string CategoryTitle { get; set; }

		public bool Selected { get; set; }
	}
}
