using System;
using System.Collections.Generic;

namespace ShopPrototype.Modules.ClientServices
{
	public class SimpleSearchCriteria
	{
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
