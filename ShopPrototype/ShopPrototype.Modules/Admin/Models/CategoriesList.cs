using ShopPrototype.Modules.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopPrototype.Modules.Admin.Models
{
	public class CategoriesList
	{
		public IEnumerable<FacilityCategory> Items { get; set; }
	}
}
