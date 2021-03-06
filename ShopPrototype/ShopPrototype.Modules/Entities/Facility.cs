﻿namespace ShopPrototype.Modules.Entities
{
	public class Facility
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public int SortOrder { get; set; }

		public int DurationMin { get; set; }

		public int FacilityCategoryId { get; set; }

		public virtual FacilityCategory FacilityCategory { get; set; }
	}
}
