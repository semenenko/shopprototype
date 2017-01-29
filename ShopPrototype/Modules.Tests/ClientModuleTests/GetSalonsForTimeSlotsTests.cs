using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShopPrototype.Modules.ClientServices;
using System.Collections.Generic;
using ShopPrototype.Modules.Entities;
using System.Linq;

namespace Modules.Tests.ClientModuleTests
{
	[TestClass]
	public class GetSalonsForTimeSlotsTests
	{
		const int SlotDurationInMin = 15;

		ClientModule GetModule()
		{
			return new ClientModule(null);
		}

		IEnumerable<SalonModel> GetSalons()
		{
			return new List<SalonModel>
			{
				new SalonModel
				{
					SalonId = 5
				},
				new SalonModel
				{
					SalonId = 10
				}
			};
		}

		IEnumerable<Facility> GetFacilities()
		{
			return new List<Facility>
			{
				new Facility
				{
					Id = 1,
					FacilityCategoryId = 1
				},
				new Facility
				{
					Id = 2,
					FacilityCategoryId = 1
				},
			};
		}

		DateTime GetTestDate()
		{
			return DateTime.Now.Date;
		}

		SalonCategoryTimeSlot GetSalonCategoryTimeSlot(int salonId, int categoryId, bool available, DateTime startDateTime)
		{
			return new SalonCategoryTimeSlot
			{
				SalonId = salonId,
				Available = available,
				CategoryId = categoryId,
				Start = startDateTime,
				End = startDateTime.AddMinutes(SlotDurationInMin)
			};
		}

		[TestMethod]
		public void OneSalonAvailable()
		{
			DateTime testDate = GetTestDate();

			IEnumerable<SalonModel> salons = GetSalons();

			IEnumerable<Facility> facilities = GetFacilities();

			IEnumerable<SalonCategoryTimeSlot> slots = new List<SalonCategoryTimeSlot>
			{
				GetSalonCategoryTimeSlot(5, 1, true, testDate.AddHours(12)),
				GetSalonCategoryTimeSlot(5, 1, true, testDate.AddHours(12).AddMinutes(15)),
				GetSalonCategoryTimeSlot(5, 1, true, testDate.AddHours(12).AddMinutes(30)),
				GetSalonCategoryTimeSlot(5, 1, true, testDate.AddHours(12).AddMinutes(45))
			};

			IEnumerable<SalonModel> result = GetModule().GetSalonsForTimeSlots(salons, facilities, slots, testDate.AddHours(12));

			Assert.AreEqual(1, result.Count());
			Assert.AreEqual(5, result.First().SalonId);
		}

		[TestMethod]
		public void TwoSalonAvailable()
		{
			DateTime testDate = GetTestDate();

			IEnumerable<SalonModel> salons = GetSalons();

			IEnumerable<Facility> facilities = GetFacilities();

			IEnumerable<SalonCategoryTimeSlot> slots = new List<SalonCategoryTimeSlot>
			{
				GetSalonCategoryTimeSlot(5, 1, true, testDate.AddHours(12)),
				GetSalonCategoryTimeSlot(5, 1, true, testDate.AddHours(12).AddMinutes(15)),
				GetSalonCategoryTimeSlot(5, 1, true, testDate.AddHours(12).AddMinutes(45)),
				GetSalonCategoryTimeSlot(5, 1, true, testDate.AddHours(13)),
				GetSalonCategoryTimeSlot(10, 1, true, testDate.AddHours(12)),
				GetSalonCategoryTimeSlot(10, 1, true, testDate.AddHours(12).AddMinutes(15)),
				GetSalonCategoryTimeSlot(10, 1, true, testDate.AddHours(12).AddMinutes(45)),
				GetSalonCategoryTimeSlot(10, 1, true, testDate.AddHours(13))
			};

			IEnumerable<SalonModel> result = GetModule().GetSalonsForTimeSlots(salons, facilities, slots, testDate.AddHours(12));

			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void OneSalonAvailableWithIntervalInSlots()
		{
			DateTime testDate = GetTestDate();

			IEnumerable<SalonModel> salons = GetSalons();

			IEnumerable<Facility> facilities = GetFacilities();

			IEnumerable<SalonCategoryTimeSlot> slots = new List<SalonCategoryTimeSlot>
			{
				GetSalonCategoryTimeSlot(5, 1, true, testDate.AddHours(12)),
				GetSalonCategoryTimeSlot(5, 1, true, testDate.AddHours(12).AddMinutes(15)),
				GetSalonCategoryTimeSlot(5, 1, true, testDate.AddHours(12).AddMinutes(45)),
				GetSalonCategoryTimeSlot(5, 1, true, testDate.AddHours(13))
			};

			IEnumerable<SalonModel> result = GetModule().GetSalonsForTimeSlots(salons, facilities, slots, testDate.AddHours(12));

			Assert.AreEqual(1, result.Count());
			Assert.AreEqual(5, result.First().SalonId);
		}

		[TestMethod]
		public void NoSalonAvailableTooBigInterval()
		{
			DateTime testDate = GetTestDate();

			IEnumerable<SalonModel> salons = GetSalons();

			IEnumerable<Facility> facilities = GetFacilities();

			IEnumerable<SalonCategoryTimeSlot> slots = new List<SalonCategoryTimeSlot>
			{
				GetSalonCategoryTimeSlot(5, 1, true, testDate.AddHours(12)),
				GetSalonCategoryTimeSlot(5, 1, true, testDate.AddHours(12).AddMinutes(15)),
				GetSalonCategoryTimeSlot(5, 1, true, testDate.AddHours(13)),
				GetSalonCategoryTimeSlot(5, 1, true, testDate.AddHours(13).AddMinutes(15))
			};

			IEnumerable<SalonModel> result = GetModule().GetSalonsForTimeSlots(salons, facilities, slots, testDate.AddHours(12));

			Assert.AreEqual(0, result.Count());
		}

		[TestMethod]
		public void NoSalonAvailableNotEnoughSlots()
		{
			DateTime testDate = GetTestDate();

			IEnumerable<SalonModel> salons = GetSalons();

			IEnumerable<Facility> facilities = GetFacilities();

			IEnumerable<SalonCategoryTimeSlot> slots = new List<SalonCategoryTimeSlot>
			{
				GetSalonCategoryTimeSlot(5, 1, true, testDate.AddHours(12)),
				GetSalonCategoryTimeSlot(5, 1, true, testDate.AddHours(12).AddMinutes(15)),
				GetSalonCategoryTimeSlot(5, 1, true, testDate.AddHours(13)),
			};

			IEnumerable<SalonModel> result = GetModule().GetSalonsForTimeSlots(salons, facilities, slots, testDate.AddHours(12));

			Assert.AreEqual(0, result.Count());
		}
	}
}
