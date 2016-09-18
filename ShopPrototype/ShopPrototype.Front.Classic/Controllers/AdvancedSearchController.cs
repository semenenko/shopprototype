using ShopPrototype.DataAccess.EF.Search;
using ShopPrototype.Modules.AdvancedSearch;
using ShopPrototype.Modules.AdvancedSearch.Models;
using System.Web.Mvc;

namespace ShopPrototype.Front.Classic.Controllers
{
	public class AdvancedSearchController : Controller
    {
		public AdvancedSearchController()
		{
			searchModule = new SearchModule(new SearchRepository());
		}

		readonly SearchModule searchModule;

		public ActionResult Search()
		{
			SearchQuery query = searchModule.GetSearchQuery();
			return View(query);
		}

		[HttpPost]
		public ActionResult Search(SearchQuery query)
		{
			ModelState.Clear();
			SearchResult model = searchModule.SearchByCoordinates(query);

			return View("SearchResult", model);
		}
	}
}