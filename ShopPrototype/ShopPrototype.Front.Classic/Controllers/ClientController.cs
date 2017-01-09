using ShopPrototype.DataAccess.EF.ClientServices;
using ShopPrototype.Modules.ClientServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopPrototype.Front.Classic.Controllers
{
    public class ClientController : Controller
    {
		public ClientController()
		{
			this.module = new ClientModule(new ClientModuleRepository());
		}

		readonly ClientModule module;

        public ActionResult Index()
        {
			IndexModel model = module.GetIndexModel();

			return View(model);
        }

		[HttpPost]
		public ActionResult SearchResult(IndexModel model)
		{
			SearchResultModel result = module.GetSearchResult(model);

			return View(result);
			//return RedirectToAction("Index");
		}
    }
}