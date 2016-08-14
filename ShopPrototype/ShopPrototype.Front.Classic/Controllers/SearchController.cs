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
	}
}