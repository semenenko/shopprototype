using System.Collections.Generic;

namespace ShopPrototype.Modules.Entities
{
	public class Salon
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public double Lat { get; set; }

		public double Long { get; set; }

		public virtual ICollection<SalonFacility> Facilities { get; set; }
	}
}
