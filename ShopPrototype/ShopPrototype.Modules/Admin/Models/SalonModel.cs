using System.Collections.Generic;

namespace ShopPrototype.Modules.Admin.Models
{
	public class SalonModel
	{
		public int Id { get; set; }

		public string SalonName { get; set; }

		public string Address { get; set; }

		public string Phone { get; set; }

		public double Lat { get; set; }

		public double Long { get; set; }

		public IEnumerable<SalonFacilityModel> Facilities { get; set; }
	}
}
