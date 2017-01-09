﻿using System;

namespace ShopPrototype.Modules.Entities
{
	public class SalonCategoryTimeSlot
	{
		public int Id { get; set; }

		public int SalonId { get; set; }

		public int CategoryId { get; set; }

		public DateTime Start { get; set; }

		public DateTime End { get; set; }

		public bool Available { get; set; }
	}
}
