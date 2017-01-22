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
		ClientModule GetModule()
		{
			return new ClientModule(null);
		}

		[TestMethod]
		public void OneSalonAvailable()
		{
			DateTime testDate = DateTime.Now.Date;

			IEnumerable<SalonModel> salons = new List<SalonModel>
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

			IEnumerable<Facility> facilities = new List<Facility>
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

			IEnumerable<SalonCategoryTimeSlot> slots = new List<SalonCategoryTimeSlot>
			{
				new SalonCategoryTimeSlot
				{
					SalonId = 5,
					Available = true,
					CategoryId = 1,
					Start = testDate.AddHours(12),
					End = testDate.AddHours(12).AddMinutes(15)
				},
				new SalonCategoryTimeSlot
				{
					SalonId = 5,
					Available = true,
					CategoryId = 1,
					Start = testDate.AddHours(12).AddMinutes(15),
					End = testDate.AddHours(12).AddMinutes(30)
				},
				new SalonCategoryTimeSlot
				{
					SalonId = 5,
					Available = true,
					CategoryId = 1,
					Start = testDate.AddHours(12).AddMinutes(30),
					End = testDate.AddHours(12).AddMinutes(45)
				},
				new SalonCategoryTimeSlot
				{
					SalonId = 5,
					Available = true,
					CategoryId = 1,
					Start = testDate.AddHours(12).AddMinutes(45),
					End = testDate.AddHours(13).AddMinutes(0)
				},
			};

			IEnumerable<SalonModel> result = GetModule().GetSalonsForTimeSlots(salons, facilities, slots, testDate.AddHours(12));

			Assert.AreEqual(1, result.Count());
			Assert.AreEqual(5, result.First().SalonId);
		}
	}
}
