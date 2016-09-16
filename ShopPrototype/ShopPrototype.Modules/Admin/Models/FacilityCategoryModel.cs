using System.Collections.Generic;
using System.Linq;

namespace ShopPrototype.Modules.Admin.Models
{
	public class FacilityCategoryModel
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public int SortOrder { get; set; }

		public IEnumerable<FacilityModel> Facilities { get; set; }

		public int NewFacilityNumber
		{
			get
			{
				if (Facilities == null || !Facilities.Any())
					return 1;

				return Facilities.Max(x => x.SortOrder) + 1;

			}
		}
	}
}
