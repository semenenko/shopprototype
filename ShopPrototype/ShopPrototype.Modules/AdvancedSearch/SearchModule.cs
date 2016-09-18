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

		public SearchQuery GetSearchQuery()
		{
			SearchQuery resut = new SearchQuery();

			using (repository.BeginUnitOfWork())
			{
				resut.Facilities = repository.GetFacilitiesSearchItems();
			}

			return resut;
		}

		public SearchResult SearchByCoordinates(SearchQuery query)
		{
			using (repository.BeginUnitOfWork())
			{
				return repository.Search(query);
			}
		}
	}
}
