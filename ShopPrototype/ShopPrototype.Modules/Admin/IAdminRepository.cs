using ShopPrototype.Modules.Admin.Models;
using ShopPrototype.Modules.Core;

namespace ShopPrototype.Modules.Admin
{
	public interface IAdminRepository  : IRepository
	{
		CategoriesList GetCategoriresList();

		void AddCategory(FacilityCategoryModel model);

		FacilityCategoryModel GetCategory(int id);

		void UpdateCategory(FacilityCategoryModel model);

		void AddFacility(FacilityModel model);

		SalonsList GetSalons(SalonQueryObject queryObject);

		SalonModel GetSalon(int id);
	}
}
