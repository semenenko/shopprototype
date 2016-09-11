using ShopPrototype.Modules.Admin.Models;
using ShopPrototype.Modules.Core;
using ShopPrototype.Modules.Entities;

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

		SalonModel GetNewSalon();

		SalonModel GetSalon(int id);

		void AddOrUpdateLocation(Salon salon);
	}
}
