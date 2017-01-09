using ShopPrototype.Modules.ClientServices;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Data.Entity.Spatial;
using ShopPrototype.Modules.Entities;

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

				IEnumerable<int> faciliesCategoriesIds = UnitOfWork.Context.Facilities
					.Where(x => criteria.Facilities.Contains(x.Id))
					.Select(x => x.FacilityCategoryId)
					.ToList();

				IEnumerable<SalonCategoryTimeSlot> categorySlotsAvailable = UnitOfWork.Context.SalonCategoryTimeSlots
					.Where(x => faciliesCategoriesIds.Contains(x.CategoryId) && x.Start >= criteria.DateTime)
					.ToList();

				var zzz = categorySlotsAvailable.ToString();
			}

			return items;
		}
	}
}
