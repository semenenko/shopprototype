using ShopPrototype.Modules.Search;
using ShopPrototype.Modules.Search.Models;
using System.Web.Mvc;

namespace ShopPrototype.Front.Classic.Controllers
{
	public class SearchController : Controller
    {
		public SearchController()
		{
			searchModule = new SearchModule();
		}

		readonly SearchModule searchModule;

		public ActionResult Index()
		{
			SearchModel model = searchModule.GetSearchModel();

			return View(model);
		}

		public ActionResult OrdinarySearch()
		{

			return View();
		}

		public ActionResult AsapSearchDateAndTime()
		{
			SearchModel model = searchModule.GetSearchModel();

			return View(model);
		}

		public ActionResult AsapSearchFacilities()
		{
			SearchModel model = searchModule.GetSearchModel();
			return View(model);
		}

		public ActionResult AsapSearchLocation()
		{
			return View();
		}
	}
}