using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopPrototype.Modules.ClientServices
{
	public class IndexModel
	{
		public IEnumerable<CategoryGroupModel> Categories { get; set; }

		public string Lat { get; set; }

		public string Long { get; set; }
	}

	public class CategoryGroupModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public int Order { get; set; }

		public IEnumerable<FacilityModel> Facilities { get; set; }
	}

	public class FacilityModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public int Order { get; set; }

		public bool Checked { get; set; }
	}
}
