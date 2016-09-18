using ShopPrototype.Modules.AdvancedSearch.Models;
using ShopPrototype.Modules.Core;
using System.Collections.Generic;

namespace ShopPrototype.Modules.AdvancedSearch
{
	public interface ISearchRepository : IRepository
	{
		SearchResult Search(SearchQuery query);

		IEnumerable<FacilitySearchItem> GetFacilitiesSearchItems();
	}
}
