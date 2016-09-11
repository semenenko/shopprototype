using ShopPrototype.Modules.AdvancedSearch.Models;
using ShopPrototype.Modules.AdvancedSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopPrototype.DataAccess.EF.Search;

namespace ShopPrototype.Front.Classic.Controllers
{
    public class AdvancedSearchController : Controller
    {
		public AdvancedSearchController()
		{
			searchModule = new SearchModule(new SearchRepository());
		}

		readonly SearchModule searchModule;

		public ActionResult SearchByMap()
		{
			return View();
		}

		[HttpPost]
		public ActionResult SearchByMap(SearchByCoordinatesQuery query)
		{
			ModelState.Clear();
			SearchResult model = searchModule.SearchByCoordinates(query);

			return View("SearchResult", model);
		}
	}
}