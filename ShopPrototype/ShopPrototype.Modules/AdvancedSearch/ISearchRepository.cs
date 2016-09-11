using ShopPrototype.Modules.AdvancedSearch.Models;
using ShopPrototype.Modules.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopPrototype.Modules.AdvancedSearch
{
	public interface ISearchRepository : IRepository
	{
		SearchResult SearchByCoordinates(SearchByCoordinatesQuery query);
	}
}
