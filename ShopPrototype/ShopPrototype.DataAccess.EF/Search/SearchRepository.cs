using ShopPrototype.Modules.AdvancedSearch;
using ShopPrototype.Modules.AdvancedSearch.Models;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;

namespace ShopPrototype.DataAccess.EF.Search
{
	public class SearchRepository : Repository, ISearchRepository
	{
		public SearchResult Search(SearchQuery query)
		{
			DbGeography myLocation = DbGeography.FromText(string.Format("POINT({0} {1})", query.Lat, query.Long).Replace(',', '.'));

			IEnumerable<SalonItem> items = UnitOfWork.Context.Locations
				.OrderBy(x => x.Location.Distance(myLocation))
				.Take(20)
				.Select(x => new SalonItem
				{
					Id = x.Id,
					Name = x.Salon.Name,
					Address = x.Salon.Address,
					FacilitiesIds = x.Salon.Facilities.Select(y => y.FacilityId).ToList()
				}).ToList();

			if (query.SelectedFacilitiesIds.Any())
			{
				items = items.Where(x => x.FacilitiesIds.Intersect(query.SelectedFacilitiesIds).Count() == query.SelectedFacilitiesIds.Count()).ToList();
			}

			SearchResult result = new SearchResult
			{
				//Lat = query.Lat,
				//Long = query.Long,
				Items = items
			};

			return result;
		}

		public IEnumerable<FacilitySearchItem> GetFacilitiesSearchItems()
		{
			return UnitOfWork.Context.Facilities
				.Select(x => new FacilitySearchItem
				{
					Id = x.Id,
					Title = x.Title,
					CategoryTitle = x.FacilityCategory.Title,
					CategorySortOrder = x.FacilityCategory.SortOrder,
					FacilitySortOrder = x.SortOrder
				}).OrderBy(x => x.CategorySortOrder).ThenBy(x => x.FacilitySortOrder).ToList();
		}
	}
}
