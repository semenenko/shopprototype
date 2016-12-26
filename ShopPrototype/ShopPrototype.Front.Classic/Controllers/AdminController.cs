using ShopPrototype.DataAccess.EF.Admin;
using ShopPrototype.DataAccess.EF.Common;
using ShopPrototype.Modules.Admin;
using ShopPrototype.Modules.Admin.Models;
using ShopPrototype.Modules.Common;
using ShopPrototype.Modules.Common.Models;
using System;
using System.Web.Mvc;

namespace ShopPrototype.Front.Classic.Controllers
{
	[Authorize]
    public class AdminController : Controller
    {
		public AdminController()
		{
			adminModule = new AdminModule(new AdminRepository());
			calendarModule = new CalendarModule(new CalendarModuleRepository());
		}

		readonly AdminModule adminModule;
		readonly CalendarModule calendarModule;

        public ActionResult CategoriesList()
		{
			CategoriesList model = adminModule.GetCategoriesList();

			return View(model);
		}

		[HttpPost]
		public ActionResult AddCategory(FacilityCategoryModel model)
		{
			adminModule.AddCategory(model);

			return RedirectToAction("CategoriesList");
		}

		public ActionResult Category(int id)
		{
			FacilityCategoryModel model = adminModule.GetCategory(id);

			return View(model);
		}

		[HttpPost]
		public ActionResult Category(FacilityCategoryModel model)
		{
			adminModule.UpdateCategory(model);

			return RedirectToAction("Category");
		}

		[HttpPost]
		public ActionResult AddFacility(FacilityModel model)
		{
			adminModule.AddFacility(model);

			return RedirectToAction("Category", new { id = model.FacilityCategoryId });
		}

		public ActionResult Salons(SalonQueryObject queryObject)
		{
			SalonsList model = adminModule.GetSalons(queryObject);

			return View(model);
		}

		public ActionResult Salon(int id)
		{
			SalonModel model = adminModule.GetSalon(id);

			return View(model);
		}

		const string SalonViewName = "Salon";

		[HttpPost]
		public ActionResult Salon(SalonModel model)
		{
			adminModule.UpdateSalon(model);

			return RedirectToAction(SalonViewName, new { id = model.Id });
		}

		public ActionResult CreateSalon()
		{
			SalonModel model = adminModule.GetNewSalon();

			return View(SalonViewName, model);
		}

		public ActionResult Calendar(int salonId, DateTime? date)
		{
			DateTime dateForCalendar = date ?? DateTime.Today;
			SalonCalendar model = calendarModule.GetSalonCalendar(salonId, dateForCalendar);

			return View(model);
		}

		[HttpPost]
		public ActionResult Calendar(SalonCalendar model)
		{
			calendarModule.UpdateSalonCalendar(model);

			return RedirectToAction("Calendar", new { salonId = model.SalonId, date = model.ScheduleDate });
		}
	}
}