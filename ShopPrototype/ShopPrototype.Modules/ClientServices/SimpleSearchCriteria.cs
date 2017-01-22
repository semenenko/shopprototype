using System;
using System.Collections.Generic;

namespace ShopPrototype.Modules.ClientServices
{
	public class SimpleSearchCriteria
	{
		public SimpleSearchCriteria() { }

		public SimpleSearchCriteria(string latitude, string longitude, IEnumerable<int> facilitiesIds)
		{
			Lat = latitude;
			Long = longitude;
			Facilities = facilitiesIds;
		}

		public DateTime DateTime
		{
			get
			{
				return ApplicationTime.GetApplicationDefaultNow();
			}
		}

		public string Lat { get; set; }

		public string Long { get; set; }

		public IEnumerable<int> Facilities { get; set; }
	}
}
