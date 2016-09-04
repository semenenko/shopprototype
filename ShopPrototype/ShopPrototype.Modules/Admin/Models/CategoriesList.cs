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

		public int NewNumber
		{
			get
			{
				if (!Items.Any())
					return 1;

				return Items.Max(x => x.SortOrder) + 1;
			}
		}
	}
}
