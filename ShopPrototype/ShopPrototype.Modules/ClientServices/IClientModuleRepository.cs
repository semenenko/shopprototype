using ShopPrototype.Modules.Core;
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

		IEnumerable<SalonModel> GetSalonsByCriteria(SimpleSearchCriteria criteria);
	}
}
