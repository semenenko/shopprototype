using ShopPrototype.Modules.Entities;
using System.Data.Entity.Spatial;

namespace ShopPrototype.DataAccess.EF.SpecificEntities
{
	public class SalonLocation
	{
		public int Id { get; set; }

		public virtual Salon Salon { get; set; }

		public DbGeography Location { get; set; }
	}
}
