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

		public SalonModel GetSalon(int id)
		{
			using (IUnitOfWork unitOfWork = repository.BeginUnitOfWork())
			{
				return repository.GetSalon(id);
			}
		}
	}
}
