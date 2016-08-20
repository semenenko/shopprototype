using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopPrototype.DataAccess.EF.SpecificEntities
{
	public class Location
	{
		public int Id { get; set; }

		public DbGeography Geography { get; set; }
	}
}
