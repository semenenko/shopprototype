using System.Collections.Generic;

namespace ShopPrototype.Modules.AdvancedSearch.Models
{
	public class SearchResult
	{
		public double Lat { get; set; }

		public double Long { get; set; }

		public IEnumerable<SalonItem> Items { get; set; }
	}

	public class SalonItem
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Address { get; set; }
	}
}
