using ShopPrototype.Modules.Core;
using ShopPrototype.Modules.Entities;
using System;
using System.Collections.Generic;

namespace ShopPrototype.Modules.Common
{
	public interface ICalendarModuleRepository : IRepository
	{
		IEnumerable<SalonFacilityTimeSlot> GetTimeSlots(int salonId, DateTime date);
	}
}
