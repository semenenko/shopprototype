using ShopPrototype.DataAccess.EF.SpecificEntities;
using ShopPrototype.Modules.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.BulkInsert.Extensions;

namespace ShopPrototype.DataAccess.EF
{
	public class InitialLoader
	{
		public void Load()
		{
			string json = File.ReadAllText("H:\\shopinfo.json");

			IEnumerable<Rootobject> objects = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Rootobject>>(json);

			IEnumerable<Salon> salons = objects.Select(x => new Salon
			{
				Name = x.Cells.Name,
				Lat = x.Lat,
				Long = x.Lon
			}).ToList();

			using (Context context = new Context())
			{
				context.BulkInsert<Salon>(salons);
				context.SaveChanges();
			}
				
			using(Context context = new Context())
			{
				IEnumerable<Salon> x = context.Salons.ToList();
				IEnumerable<SalonLocation> y = x.Select(z => new SalonLocation
				{
					Id = z.Id,
					Location = DbGeography.FromText(string.Format("POINT({0} {1})", z.Lat, z.Long).Replace(',', '.'))
				}).ToList();

				context.BulkInsert(y);
				context.SaveChanges();
			}
		}

		public string TestLocation(double lat, double lon)
		{
			DbGeography myLocation = DbGeography.FromText(string.Format("POINT({0} {1})", lat, lon).Replace(',', '.'));

			using (Context context = new Context())
			{
				Salon salon = context.Locations.OrderBy(x => x.Location.Distance(myLocation)).FirstOrDefault().Salon;
				return string.Format("{0}, {1}, {2}", salon.Name, salon.Lat, salon.Long);
			}
		}
	}

	public class Rootobject
	{
		public double Lat { get; set; }
		public double Lon { get; set; }
		public string Id { get; set; }
		public int Number { get; set; }
		public Cells Cells { get; set; }
	}

	public class Cells
	{
		public int global_id { get; set; }
		public string Name { get; set; }
		public string IsNetObject { get; set; }
		public object OperatingCompany { get; set; }
		public string TypeService { get; set; }
		public string AdmArea { get; set; }
		public string District { get; set; }
		public string Address { get; set; }
		public Publicphone[] PublicPhone { get; set; }
		public Workinghour[] WorkingHours { get; set; }
		public object ClarificationOfWorkingHours { get; set; }
		public string Longitude_WGS84 { get; set; }
		public string Latitude_WGS84 { get; set; }
	}

	public class Publicphone
	{
		public object PublicPhone { get; set; }
	}

	public class Workinghour
	{
		public string DayOfWeek { get; set; }
		public string Hours { get; set; }
	}
}
