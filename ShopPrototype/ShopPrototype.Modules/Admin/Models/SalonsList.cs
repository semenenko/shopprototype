using ShopPrototype.Modules.Core;
using System.Collections.Generic;

namespace ShopPrototype.Modules.Admin.Models
{
	public class SalonsList
	{
		public IEnumerable<SalonListItem> Items { get; set; }

		public SalonQueryObject QueryObject { get; set; }

		public ColumnHeader NameHeader
		{
			get
			{
				return new ColumnHeader
				{
					CurrentSort = QueryObject == null ? string.Empty : QueryObject.SortBy,
					DisplayName = "Название",
					SortKey = "name"
				};
			}
		}
	}
}
