using ShopPrototype.Modules.Admin.Models;
using ShopPrototype.Modules.Core;
using ShopPrototype.Modules.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ShopPrototype.Modules.Admin
{
	public class AdminModule
	{
		public AdminModule(IAdminRepository repository)
		{
			this.repository = repository;
		}

		readonly IAdminRepository repository;

		public CategoriesList GetCategoriesList()
		{
			using(IUnitOfWork unitOfWork= repository.BeginUnitOfWork())
			{
				CategoriesList result = repository.GetCategoriresList();

				return result;
			}
		}

		public void AddCategory(FacilityCategoryModel model)
		{
			using(IUnitOfWork unitOfWork = repository.BeginUnitOfWork())
			{
				repository.AddCategory(model);

				unitOfWork.Commit();
			}
		}

		public FacilityCategoryModel GetCategory(int id)
		{
			using (IUnitOfWork unitOfWork = repository.BeginUnitOfWork())
			{
				FacilityCategoryModel result = repository.GetCategory(id);

				return result;
			}
		}

		public void UpdateCategory(FacilityCategoryModel model)
		{
			using (IUnitOfWork unitOfWork = repository.BeginUnitOfWork())
			{
				repository.UpdateCategory(model);

				unitOfWork.Commit();
			}
		}

		public void AddFacility(FacilityModel model)
		{
			using (IUnitOfWork unitOfWork = repository.BeginUnitOfWork())
			{
				repository.AddFacility(model);

				unitOfWork.Commit();
			}
		}

		public SalonsList GetSalons(SalonQueryObject queryObject)
		{
			using (IUnitOfWork unitOfWork = repository.BeginUnitOfWork())
			{
				if (queryObject == null)
					queryObject = new SalonQueryObject { PageSize = 20, CurrentPage = 1 };

				return repository.GetSalons(queryObject);
			}
		}

		public SalonModel GetNewSalon()
		{
			using (repository.BeginUnitOfWork())
				return repository.GetNewSalon();
		}

		public SalonModel GetSalon(int id)
		{
			using (IUnitOfWork unitOfWork = repository.BeginUnitOfWork())
			{
				return repository.GetSalon(id);
			}
		}

		public void UpdateSalon(SalonModel model)
		{
			using (IUnitOfWork unitOfWork = repository.BeginUnitOfWork())
			{
				Salon salon = repository.GetEntity<Salon>(model.Id);

				if (salon == null)
				{
					salon = new Salon();
					repository.AddEntity(salon);
				}

				model.UpdateEntity(salon);

				if (model.LocationChanged)
				{
					repository.AddOrUpdateLocation(salon);
				}

				IEnumerable<SalonFacilityModel> facilitiesInModel = model.Facilities.Where(x => x.Selected).ToList();

				UpdateSalonFacilities(salon, facilitiesInModel);

				unitOfWork.Commit();

				model.Id = salon.Id;
			}
		}

		void UpdateSalonFacilities(Salon salon, IEnumerable<SalonFacilityModel> models)
		{
			IEnumerable<SalonFacility> entities = salon.Facilities != null ? salon.Facilities.ToList() : new List<SalonFacility>();

			foreach(SalonFacility entity in entities)
			{
				SalonFacilityModel model = models.FirstOrDefault(x => x.FacilityId == entity.FacilityId);

				if (model == null)
				{
					salon.Facilities.Remove(entity);
					continue;
				}

				entity.DurationMin = model.DurationMin;
			}

			foreach(SalonFacilityModel model in models)
			{
				if (entities.Any(x => x.FacilityId == model.FacilityId))
					continue;

				SalonFacility entity = new SalonFacility
				{
					FacilityId = model.FacilityId,
					DurationMin = model.DurationMin
				};

				salon.Facilities.Add(entity);
			}
		}
	}
}
