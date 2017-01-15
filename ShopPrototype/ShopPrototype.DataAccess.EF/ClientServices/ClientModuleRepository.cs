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

			IEnumerable<SalonModel> salonsByLocation = UnitOfWork.Context.Locations
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
				IEnumerable<SalonModel> salonsByLocationsAndFacilities = FilterSalonsByFacilities(salonsByLocation, criteria.Facilities);

				IEnumerable<Facility> criteriaFacilities = GetFacilities(criteria.Facilities);

				IEnumerable<int> criteriaFaciliesCategoriesIds = criteriaFacilities.Select(x => x.FacilityCategoryId).ToList();

				IEnumerable<SalonCategoryTimeSlot> allSlotsAvailable = UnitOfWork.Context.SalonCategoryTimeSlots
					.Where(x => criteriaFaciliesCategoriesIds.Contains(x.CategoryId) && x.Start >= criteria.DateTime)
					.ToList();

				foreach (IGrouping<int, Facility> facilitiesGroup in criteriaFacilities.GroupBy(x => x.FacilityCategoryId))
				{
					int defaultDuration = 30;
					int groupDurationInMin = facilitiesGroup.Sum(x => x.DurationMin != 0 ? x.DurationMin : defaultDuration);

					IEnumerable<SalonCategoryTimeSlot> groupSlotsAvailable = allSlotsAvailable.Where(x => x.CategoryId == facilitiesGroup.Key).ToList();


				}

				var zzz = categorySlotsAvailable.ToString();
			}

			return salonsByLocation;
		}

		IEnumerable<Facility> GetFacilities(IEnumerable<int> ids)
		{
			return UnitOfWork.Context.Facilities.Where(x => ids.Contains(x.Id)).ToList();
		}

		IEnumerable<SalonModel> FilterSalonsByFacilities(IEnumerable<SalonModel> salons, IEnumerable<int> criteriaFacilitiesIds)
		{
			return salons.Where(x => x.Facilities.Intersect(criteriaFacilitiesIds).Count() == criteriaFacilitiesIds.Count()).ToList();
		}
	}
}
