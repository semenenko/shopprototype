using ShopPrototype.Modules.ClientServices;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Data.Entity.Spatial;

namespace ShopPrototype.DataAccess.EF.ClientServices
{
	public class ClientModuleRepository : Repository, IClientModuleRepository
	{
		public IEnumerable<CategoryGroupModel> GetFacilitiesByGroups()
		{
			return UnitOfWork.Context.FacilityCategories
				.Select(x => new CategoryGroupModel
				{
					Id = x.Id,
					Order = x.SortOrder,
					Name = x.Title,
					Facilities = x.Facilities.Select(y => new FacilityModel
					{
						Id = y.Id,
						Name = y.Title,
						Order = y.SortOrder
					}).OrderBy(z => z.Order)
				})
				.OrderBy(x => x.Order)
				.ToList();
		}

		public IEnumerable<SalonModel> GetSalonsByCriteria(SimpleSearchCriteria criteria)
		{
			DbGeography myLocation = DbGeography.FromText(string.Format("POINT({0} {1})", criteria.Lat, criteria.Long).Replace(',', '.'));

			IEnumerable<SalonModel> items = UnitOfWork.Context.Locations
				.OrderBy(x => x.Location.Distance(myLocation))
				.Take(20)
				.Select(x => new SalonModel
				{
					SalonId = x.Id,
					SalonName = x.Salon.Name,
					Address = x.Salon.Address,
					Facilities = x.Salon.Facilities.Select(y => y.FacilityId).ToList()
				}).ToList();

			if (criteria.Facilities.Any())
			{
				items = items.Where(x => x.Facilities.Intersect(criteria.Facilities).Count() == criteria.Facilities.Count()).ToList();
			}

			return items;
		}
	}
}
