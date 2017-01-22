using ShopPrototype.Modules.Core;
using ShopPrototype.Modules.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopPrototype.Modules.ClientServices
{
	public interface IClientModuleRepository : IRepository
	{
		IEnumerable<CategoryGroupModel> GetFacilitiesByGroups();

		IEnumerable<SalonModel> GetNearestSalons(string latitude, string longitude, int count);

		IEnumerable<SalonCategoryTimeSlot> GetSlotsAvailable(DateTime datetime, IEnumerable<int> criteriaFaciliesCategoriesIds);

		IEnumerable<Facility> GetFacilities(IEnumerable<int> ids);
	}
}
