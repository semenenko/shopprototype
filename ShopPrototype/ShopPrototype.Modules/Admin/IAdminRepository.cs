﻿using ShopPrototype.Modules.Admin.Models;
using ShopPrototype.Modules.Core;

namespace ShopPrototype.Modules.Admin
{
	public interface IAdminRepository  : IRepository
	{
		CategoriesList GetCategoriresList();
	}
}