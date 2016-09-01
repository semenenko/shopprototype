using ShopPrototype.Modules.Admin.Models;
using ShopPrototype.Modules.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
				return repository.GetCategoriresList();
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
	}
}
