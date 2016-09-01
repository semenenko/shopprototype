using ShopPrototype.DataAccess.EF.Admin;
using ShopPrototype.Modules.Admin;
using ShopPrototype.Modules.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopPrototype.Front.Classic.Controllers
{
	[Authorize]
    public class AdminController : Controller
    {
		public AdminController()
		{
			adminModule = new AdminModule(new AdminRepository());
		}

		readonly AdminModule adminModule;

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
	}
}