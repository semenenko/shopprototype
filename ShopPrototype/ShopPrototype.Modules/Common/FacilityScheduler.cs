using ShopPrototype.Modules.Common.Models;
using ShopPrototype.Modules.Entities;
using System;
using System.Collections.Generic;

namespace ShopPrototype.Modules.Common
{
	public class FacilityScheduler
	{
		public FacilityDaySchedule GetFacilityDaySchedule(SalonFacility salonFacility, DateTime date, DateTime start, DateTime end)
		{
			FacilityDaySchedule result = new FacilityDaySchedule
			{
				SalonId = salonFacility.SalonId,
				FacilityId = salonFacility.FacilityId,
				Date = date
			};

			int durationInMin = salonFacility.DurationMin ?? salonFacility.Facility.DurationMin;

			DateTime currentStart = start;
			List<ScheduleItemModel> items = new List<ScheduleItemModel>();

			while(start < end)
			{
				ScheduleItemModel item = new ScheduleItemModel
				{
					SalonId = salonFacility.SalonId,
					FacilityId = salonFacility.FacilityId,
					DurationMin = durationInMin,
					Available = false,
					StartDateTime = currentStart
				};

				items.Add(item);

				currentStart = currentStart.AddMinutes(durationInMin);
			}

			result.Items = items;

			return result;
		}
	}
}
