using ShopPrototype.Modules.Common;
using ShopPrototype.Modules.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopPrototype.DataAccess.EF.Common
{
	public class CalendarModuleRepository : Repository, ICalendarModuleRepository
	{
		public IEnumerable<SalonFacilityTimeSlot> GetTimeSlots(int salonId, DateTime date)
		{
			DateTime dateTo = date.AddDays(1);

			return UnitOfWork.Context.SalonFacilityTimeSlots
				.Where(x => x.SalonId == salonId)
				.Where(x => x.SlotStartsAt >= date && x.SlotStartsAt < dateTo)
				.ToList();
		}

		public IEnumerable<SalonCategoryTimeSlot> GetCategorySlots(int salonId, DateTime date)
		{
			DateTime dateTo = date.AddDays(1);

			return UnitOfWork.Context.SalonCategoryTimeSlots
				.Where(x => x.SalonId == salonId)
				.Where(x => x.Start >= date && x.End < dateTo)
				.ToList();
		}
	}
}
