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

		public IEnumerable<SalonModel> GetNearestSalons(string latitude, string longitude, int count)
		{
			DbGeography myLocation = DbGeography.FromText(string.Format("POINT({0} {1})", latitude, longitude).Replace(',', '.'));

			IEnumerable<SalonModel> result = UnitOfWork.Context.Locations
				.OrderBy(x => x.Location.Distance(myLocation))
				.Take(count)
				.Select(x => new SalonModel
				{
					SalonId = x.Id,
					SalonName = x.Salon.Name,
					Address = x.Salon.Address,
					Facilities = x.Salon.Facilities.Select(y => y.FacilityId).ToList()
				}).ToList();

			return result;
		}

		public IEnumerable<SalonCategoryTimeSlot> GetSlotsAvailable(DateTime datetime, IEnumerable<int> criteriaFaciliesCategoriesIds)
		{
			return UnitOfWork.Context.SalonCategoryTimeSlots
					.Where(x => x.Available && x.Start >= datetime && criteriaFaciliesCategoriesIds.Contains(x.CategoryId))
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

				//I need permutation here

				foreach (IGrouping<int, Facility> facilitiesGroup in criteriaFacilities.GroupBy(x => x.FacilityCategoryId))
				{
					int defaultDuration = 30;
					int groupDurationInMin = facilitiesGroup.Sum(x => x.DurationMin != 0 ? x.DurationMin : defaultDuration);

					IEnumerable<SalonCategoryTimeSlot> groupSlotsAvailable = allSlotsAvailable.Where(x => x.CategoryId == facilitiesGroup.Key).ToList();


				}

				//var zzz = categorySlotsAvailable.ToString();
			}

			return salonsByLocation;
		}

		public IEnumerable<Facility> GetFacilities(IEnumerable<int> ids)
		{
			return UnitOfWork.Context.Facilities.Where(x => ids.Contains(x.Id)).ToList();
		}

		IEnumerable<SalonModel> FilterSalonsByFacilities(IEnumerable<SalonModel> salons, IEnumerable<int> criteriaFacilitiesIds)
		{
			return salons.Where(x => x.Facilities.Intersect(criteriaFacilitiesIds).Count() == criteriaFacilitiesIds.Count()).ToList();
		}

		//http://stackoverflow.com/questions/7802822/all-possible-combinations-of-a-list-of-values
		// Recursive
		public static List<List<T>> GetAllCombos<T>(List<T> list)
		{
			List<List<T>> result = new List<List<T>>();
			// head
			result.Add(new List<T>());
			result.Last().Add(list[0]);
			if (list.Count == 1)
				return result;
			// tail
			List<List<T>> tailCombos = GetAllCombos(list.Skip(1).ToList());
			tailCombos.ForEach(combo =>
			{
				result.Add(new List<T>(combo));
				combo.Add(list[0]);
				result.Add(new List<T>(combo));
			});
			return result;
		}

		// Iterative, using 'i' as bitmask to choose each combo members
		//public static List<List<T>> GetAllCombos<T>(List<T> list)
		//{
		//	int comboCount = (int)Math.Pow(2, list.Count) - 1;
		//	List<List<T>> result = new List<List<T>>();
		//	for (int i = 1; i < comboCount + 1; i++)
		//	{
		//		// make each combo here
		//		result.Add(new List<T>());
		//		for (int j = 0; j < list.Count; j++)
		//		{
		//			if ((i >> j) % 2 != 0)
		//				result.Last().Add(list[j]);
		//		}
		//	}
		//	return result;
		//}
	}
}
