using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopPrototype.Modules.Search;
using ShopPrototype.Modules.Search.Models;

namespace ShopPrototype.Front01.Controllers
{
    public class SearchController : Controller
    {
		public SearchController()
		{
			searchModule = new SearchModule();
		}

		readonly SearchModule searchModule;

        public IActionResult Index()
        {
			SearchModel model = searchModule.GetSearchModel();

            return View(model);
        }
    }
}
