using ShopPrototype.Modules.AdvancedSearch;
using ShopPrototype.Modules.AdvancedSearch.Models;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;

namespace ShopPrototype.DataAccess.EF.Search
{
	public class SearchRepository : Repository, ISearchRepository
	{
		public SearchResult SearchByCoordinates(SearchByCoordinatesQuery query)
		{
			DbGeography myLocation = DbGeography.FromText(string.Format("POINT({0} {1})", query.Lat, query.Long).Replace(',', '.'));

			IEnumerable<SalonItem> items = UnitOfWork.Context.Locations
				.OrderBy(x => x.Location.Distance(myLocation))
				.Take(20)
				.Select(x => new SalonItem
				{
					Id = x.Id,
					Name = x.Salon.Name,
					Address = x.Salon.Address
				}).ToList();

			SearchResult result = new SearchResult
			{
				//Lat = query.Lat,
				//Long = query.Long,
				Items = items
			};

			return result;
		}
	}
}
