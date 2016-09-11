using ShopPrototype.Modules.Entities;
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

		public bool LocationChanged { get; private set; }

		public void UpdateEntity(Salon entity)
		{
			entity.Name = SalonName;
			entity.Address = Address;
			entity.Phone = Phone;

			if (entity.Lat != Lat || entity.Long != Long)
			{
				entity.Lat = Lat;
				entity.Long = Long;
				LocationChanged = true;
			}
		}
	}
}
