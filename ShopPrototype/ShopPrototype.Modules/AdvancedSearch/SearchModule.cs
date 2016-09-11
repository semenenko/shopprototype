using ShopPrototype.Modules.AdvancedSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopPrototype.Modules.AdvancedSearch
{
	public class SearchModule
	{
		public SearchModule(ISearchRepository repository)
		{
			this.repository = repository;
		}

		readonly ISearchRepository repository;

		public SearchResult SearchByCoordinates(SearchByCoordinatesQuery query)
		{
			using (repository.BeginUnitOfWork())
			{
				return repository.SearchByCoordinates(query);
			}
		}
	}
}
