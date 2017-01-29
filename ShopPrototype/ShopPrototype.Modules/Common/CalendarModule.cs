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

		public DaySchedule GetDayScheduleForSalon(int salonId, DateTime? date)
		{
			DateTime scheduleDate = date ?? ApplicationTime.GetApplicationDefaultNow().Date;

			using (repository.BeginUnitOfWork())
			{
				IEnumerable<SalonCategoryTimeSlot> storedSlotsForAllFacilities = repository.GetCategorySlots(salonId, scheduleDate);

				Salon salon = repository.GetEntity<Salon>(salonId);
				IEnumerable<FacilityCategory> categories = salon.Facilities
					.Select(x => x.Facility.FacilityCategory)
					.Distinct()
					.ToList();

				int slotDurationMin = 15;

				List<ScheduleItem> scheduleItemsList = new List<ScheduleItem>();

				foreach(FacilityCategory category in categories)
				{
					DateTime currentSlotStart = scheduleDate.Add(salon.OpensAt);
					DateTime salonClosesAt = scheduleDate.Add(salon.ClosesAt);

					while (true)
					{
						ScheduleItem item = new ScheduleItem
						{
							CategoryId = category.Id,
							CategoryName = category.Title,
							ItemStartsAt = currentSlotStart,
							ItemEndsAt = currentSlotStart.AddMinutes(slotDurationMin)
						};

						SalonCategoryTimeSlot storedSlot = storedSlotsForAllFacilities.FirstOrDefault(x => x.SalonId == salonId
						&& x.CategoryId == category.Id
						&& x.Start == item.ItemStartsAt
						&& x.End == item.ItemEndsAt);

						if (storedSlot != null)
							item.Available = storedSlot.Available;

						scheduleItemsList.Add(item);

						currentSlotStart = currentSlotStart.AddMinutes(slotDurationMin);

						if (currentSlotStart > salonClosesAt)
							break;
					}
				}

				DaySchedule schedule = new DaySchedule();
				schedule.Initialize(salonId, salon.Name, scheduleDate, scheduleItemsList);

				return schedule;
			}
		}

		public void UpdateSalonDaySchedule(DaySchedule model)
		{
			using (IUnitOfWork unitOfWork = repository.BeginUnitOfWork())
			{
				IEnumerable<SalonCategoryTimeSlot> storedSlots = repository.GetCategorySlots(model.Id, model.CurrentDate);

				IEnumerable<ScheduleItem> modelItems = model.Rows.SelectMany(x => x.Items);

				foreach (ScheduleItem modelItem in modelItems)
				{
					SalonCategoryTimeSlot storedSlot = storedSlots.FirstOrDefault(x => x.SalonId == model.Id
						&& x.CategoryId == modelItem.CategoryId
						&& x.Start == modelItem.ItemStartsAt
						&& x.End == modelItem.ItemEndsAt);

					if (storedSlot == null && modelItem.Available)
					{
						storedSlot = new SalonCategoryTimeSlot
						{
							SalonId = model.Id,
							CategoryId = modelItem.CategoryId,
							Start = modelItem.ItemStartsAt,
							End = modelItem.ItemEndsAt
						};

						repository.AddEntity(storedSlot);
					}

					if (storedSlot != null)
						storedSlot.Available = modelItem.Available;
				}

				unitOfWork.Commit();
			}
		}
	}
}
