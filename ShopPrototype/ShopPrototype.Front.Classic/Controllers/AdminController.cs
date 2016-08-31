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
        public ActionResult CategoriesList()
		{
			CategoriesList model = new CategoriesList();

			return View(model);
		}

	}
}