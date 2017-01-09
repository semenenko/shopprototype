using System;
using System.Collections.Generic;

namespace ShopPrototype.Modules.Entities
{
	public class Salon
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public double Lat { get; set; }

		public double Long { get; set; }

		public string AdministrativeArea { get; set; }

		public string District { get; set; }

		public string Address { get; set; }

		public string Phone { get; set; }

		public TimeSpan OpensAt { get { return new TimeSpan(10, 0, 0); } }

		public TimeSpan ClosesAt { get { return new TimeSpan(22, 0, 0); } }

		public virtual ICollection<SalonFacility> Facilities { get; set; }
	}
}
