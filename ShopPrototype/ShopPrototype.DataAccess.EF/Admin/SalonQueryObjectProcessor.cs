using ShopPrototype.Modules.Admin.Models;
using ShopPrototype.Modules.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ShopPrototype.DataAccess.EF.Admin
{
	class SalonQueryObjectProcessor : QueryObjectProcessor<SalonQueryObject, Salon, SalonListItem>
	{
		public SalonQueryObjectProcessor(SalonQueryObject queryObject) : base(queryObject)
		{
		}

		public override IEnumerable<SortCommand<Salon>> SortCommands
		{
			get
			{
				return new List<SortCommand<Salon>>
				{
					new SortCommand<Salon, string> { Key = "name_asc", Selector = x => x.Name },
					new SortCommand<Salon, string> { Key = "name_desc", Selector = x => x.Name }
				};
			}
		}

		protected override void ApplyFilter()
		{
			if (!string.IsNullOrWhiteSpace(QueryObject.Name))
				Query = Query.Where(x => x.Name.Contains(QueryObject.Name));
		}

		protected override void ApplySelect()
		{
			Output = Query.Select(x => new SalonListItem
			{
				Id = x.Id,
				Name = x.Name
			}).ToList();
		}

		protected override SalonQueryObject GetDefaultQueryObject()
		{
			return new SalonQueryObject
			{
				CurrentPage = 1,
				PageSize = 20
			};
		}

		protected override void ApplyDefaultSort()
		{
			Query = Query.OrderBy(x => x.Id);
		}
	}
}
