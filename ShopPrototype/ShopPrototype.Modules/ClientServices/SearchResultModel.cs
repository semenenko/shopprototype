using System.Collections.Generic;

namespace ShopPrototype.Modules.ClientServices
{
	public class SearchResultModel
	{
		public string Lat { get; set; }

		public string Long { get; set; }

		public IEnumerable<SelectedFacilities> Facilities { get; set; }

		public IEnumerable<SalonModel> Salons { get; set; }
	}

	public class SelectedFacilities
	{
		public int FacilityId { get; set; }

		public string FacilityName { get; set; }
	}

	public class SalonModel
	{
		public int SalonId { get; set; }

		public string SalonName { get; set; }

		public string Address { get; set; }

		public IEnumerable<int> Facilities { get; set; }
	}
}
