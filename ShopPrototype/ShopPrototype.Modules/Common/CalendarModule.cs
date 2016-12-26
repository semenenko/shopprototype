using ShopPrototype.Modules.Common.Models;
using ShopPrototype.Modules.Core;
using ShopPrototype.Modules.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopPrototype.Modules.Common
{
	public class CalendarModule
	{
		public CalendarModule(ICalendarModuleRepository repository)
		{
			this.repository = repository;
		}

		readonly ICalendarModuleRepository repository;

		public SalonCalendar GetSalonCalendar(int salonId, DateTime date)
		{
			using (repository.BeginUnitOfWork())
			{
				Salon salon = repository.GetEntity<Salon>(salonId);
				IEnumerable<SalonFacility> facilities = salon.Facilities.ToList();

				IEnumerable<CalendarFacilityItem> facilitiyModels = GenerateFacilities(salonId, date, facilities);

				SalonCalendar calendar = new SalonCalendar
				{
					SalonId = salon.Id,
					ScheduleDate = date,
					SalonName = salon.Name,
					Facilities = facilitiyModels
				};

				return calendar;
			}
		}

		IEnumerable<CalendarFacilityItem> GenerateFacilities(int salonId, DateTime date, IEnumerable<SalonFacility> facilities)
		{
			IEnumerable<SalonFacilityTimeSlot> storedSlotsForAllFacilities = repository.GetTimeSlots(salonId, date);

			IEnumerable<CalendarFacilityItem> calendarFacilities = facilities.OrderBy(x => x.Facility.SortOrder)
				.Select(x => new CalendarFacilityItem
				{
					FacilityId = x.FacilityId,
					FacilityName = x.Facility.Title
				}).ToList();

			foreach (CalendarFacilityItem item in calendarFacilities)
			{
				IEnumerable<SalonFacilityTimeSlot> slotsForFacility = storedSlotsForAllFacilities.Where(x => x.FacilityId == item.FacilityId).ToList();
				item.TimeSlots = GenerateSlots(date, slotsForFacility);
			}
				

			return calendarFacilities;
		}

		IEnumerable<FacilityTimeSlotItem> GenerateSlots(DateTime date, IEnumerable<SalonFacilityTimeSlot> storedSlots)
		{
			int slotDurationMin = 15;
			int salonStartsAtHours = 10;
			int salonStartsAtMins = 0;
			int salonEndsAtHours = 22;
			int salonEnsAtMins = 0;

			List<FacilityTimeSlotItem> result = new List<FacilityTimeSlotItem>();

			DateTime nextItemStart = new DateTime(date.Year, date.Month, date.Day).AddHours(salonStartsAtHours).AddMinutes(salonStartsAtMins);
			DateTime salonEndsAt = new DateTime(date.Year, date.Month, date.Day).AddHours(salonEndsAtHours).AddMinutes(salonEnsAtMins);

			while (true)
			{
				FacilityTimeSlotItem item = new FacilityTimeSlotItem
				{
					Id = Guid.NewGuid(),
					Available = false,
					SlotStartsAt = nextItemStart,
					SlotEndsAt = nextItemStart.AddMinutes(slotDurationMin)
				};

				result.Add(item);

				nextItemStart = nextItemStart.AddMinutes(slotDurationMin);
				if (nextItemStart.Date > salonEndsAt)
					break;
			}

			foreach(FacilityTimeSlotItem item in result)
			{
				SalonFacilityTimeSlot storedSlot = storedSlots.Where(x => x.SlotStartsAt == item.SlotStartsAt && x.SlotEndsAt == item.SlotEndsAt).FirstOrDefault();
				if (storedSlot == null)
					continue;

				item.Available = storedSlot.Available;
			}

			return result;
		}

		public void UpdateSalonCalendar(SalonCalendar model)
		{
			using(IUnitOfWork unitOfWork = repository.BeginUnitOfWork())
			{
				IEnumerable<SalonFacilityTimeSlot> storedSlots = repository.GetTimeSlots(model.SalonId, model.ScheduleDate);

				foreach(CalendarFacilityItem facility in model.Facilities)
				{
					foreach(FacilityTimeSlotItem slotItem in facility.TimeSlots)
					{
						SalonFacilityTimeSlot storedSlot = storedSlots
							.FirstOrDefault(x => x.FacilityId == facility.FacilityId
							&& x.SlotStartsAt == slotItem.SlotStartsAt
							&& x.SlotEndsAt == slotItem.SlotEndsAt);

						if (storedSlot == null && slotItem.Available)
						{
							storedSlot = new SalonFacilityTimeSlot
							{
								SalonId = model.SalonId,
								FacilityId = facility.FacilityId,
								SlotStartsAt = slotItem.SlotStartsAt,
								SlotEndsAt = slotItem.SlotEndsAt
							};

							repository.AddEntity(storedSlot);
						}

						if (storedSlot != null)
							storedSlot.Available = slotItem.Available;
					}
				}

				unitOfWork.Commit();
			}
		}
	}
}
