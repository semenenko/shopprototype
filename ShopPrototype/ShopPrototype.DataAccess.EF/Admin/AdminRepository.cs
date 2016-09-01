using ShopPrototype.Modules.Admin;
using ShopPrototype.Modules.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopPrototype.Modules.Admin.Models;
using ShopPrototype.Modules.Entities;
using System.Data.Entity;

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

		public void UpdateCategory(FacilityCategoryModel model)
		{
			FacilityCategory entity = UnitOfWork.Context.FacilityCategories.Include(x => x.Facilities)
				.FirstOrDefault(x => x.Id == model.Id);

			if (entity == null)
				throw new InvalidOperationException("Facility category with id = {0} not found.");

			entity.Title = model.Title;
			entity.SortOrder = model.SortOrder;
		}
	}
}
