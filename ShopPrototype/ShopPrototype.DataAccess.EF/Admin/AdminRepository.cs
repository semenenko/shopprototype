using ShopPrototype.DataAccess.EF.SpecificEntities;
using ShopPrototype.Modules.Admin;
using ShopPrototype.Modules.Admin.Models;
using ShopPrototype.Modules.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Spatial;
using System.Linq;

namespace ShopPrototype.DataAccess.EF.Admin
{
	public class AdminRepository : Repository, IAdminRepository
	{
		public CategoriesList GetCategoriresList()
		{
			CategoriesList result = new CategoriesList();

			result.Items = UnitOfWork.Context.FacilityCategories
				.OrderBy(x => x.SortOrder)
				.ToList();

			return result;
		}

		public void AddCategory(FacilityCategoryModel model)
		{
			FacilityCategory entity = new FacilityCategory
			{
				SortOrder = model.SortOrder,
				Title = model.Title
			};

			UnitOfWork.Context.FacilityCategories.Add(entity);
		}

		public FacilityCategoryModel GetCategory(int id)
		{
			FacilityCategoryModel result = UnitOfWork.Context.FacilityCategories
				.Where(x => x.Id == id)
				.Select(x => new FacilityCategoryModel
				{
					Id = x.Id,
					Title = x.Title,
					SortOrder = x.SortOrder,
					Facilities = x.Facilities.Select(y => new FacilityModel
					{
						Id = y.Id,
						Title = y.Title,
						SortOrder = y.SortOrder,
						DurationMin = y.DurationMin
					}).OrderBy(y => y.SortOrder)
				}).FirstOrDefault();

			return result;
		}

		public void UpdateCategory(FacilityCategoryModel model)
		{
			FacilityCategory entity = UnitOfWork.Context.FacilityCategories.Include(x => x.Facilities)
				.FirstOrDefault(x => x.Id == model.Id);

			if (entity == null)
				throw new InvalidOperationException("Facility category with id = {0} not found.");

			entity.Title = model.Title;
			entity.SortOrder = model.SortOrder;

			foreach(FacilityModel facilityModel in model.Facilities)
			{
				Facility facilityEntity = entity.Facilities.First(x => x.Id == facilityModel.Id);
				facilityEntity.Title = facilityModel.Title;
				facilityEntity.SortOrder = facilityModel.SortOrder;
				facilityEntity.DurationMin = facilityModel.DurationMin;
			}
		}

		public void AddFacility(FacilityModel model)
		{
			Facility entity = new Facility
			{
				FacilityCategoryId = model.FacilityCategoryId,
				SortOrder = model.SortOrder,
				Title = model.Title,
				DurationMin = model.DurationMin
			};

			UnitOfWork.Context.Facilities.Add(entity);
		}

		public SalonsList GetSalons(SalonQueryObject queryObject)
		{
			IQueryable<Salon> salonsQuery = UnitOfWork.Context.Salons.Where(x => x.Id != 0);
			SalonsList result = new SalonsList();

			SalonQueryObjectProcessor queryProcessor = new SalonQueryObjectProcessor(queryObject);

			result.Items = queryProcessor.ProcessQuery(salonsQuery);
			result.QueryObject = queryProcessor.QueryObject;

			return result;
		}

		IEnumerable<SalonFacilityModel> GetFacilitiesModels()
		{
			IEnumerable<SalonFacilityModel> facilities = UnitOfWork.Context.Facilities
				.Select(x => new SalonFacilityModel
				{
					FacilityId = x.Id,
					FacilityCategoryId = x.FacilityCategoryId,
					FacilityTitle = x.Title,
					CategoryTitle = x.FacilityCategory.Title
				}).OrderBy(x => x.CategoryTitle).ThenBy(x => x.FacilityTitle).ToList();

			return facilities;
		}

		public SalonModel GetNewSalon()
		{
			SalonModel result = new SalonModel
			{
				Facilities = GetFacilitiesModels()
			};

			return result;
		}

		public SalonModel GetSalon(int id)
		{
			Salon salon = UnitOfWork.Context.Salons
				.Include(x => x.Facilities)
				.FirstOrDefault(x => x.Id == id);

			IEnumerable<SalonFacilityModel> facilities = GetFacilitiesModels();

			foreach (SalonFacility facility in salon.Facilities)
			{
				SalonFacilityModel modelFacility = facilities.First(x => x.FacilityId == facility.FacilityId);
				modelFacility.Selected = true;
				modelFacility.DurationMin = facility.DurationMin;
			}

			SalonModel result = new SalonModel
			{
				Id = salon.Id,
				SalonName = salon.Name,
				Address = salon.Address,
				Phone = salon.Phone,
				Lat = salon.Lat,
				Long = salon.Long,
				Facilities = facilities
			};

			return result;
		}

		public void AddOrUpdateLocation(Salon salon)
		{
			SalonLocation location = UnitOfWork.Context.Locations.FirstOrDefault(x => x.Id == salon.Id);

			if (location == null)
			{
				location = new SalonLocation
				{
					Id = salon.Id
				};
				UnitOfWork.Context.Locations.Add(location);
			}

			location.Location = DbGeography.FromText(string.Format("POINT({0} {1})", salon.Lat, salon.Long).Replace(',', '.'));
		}
	}
}
