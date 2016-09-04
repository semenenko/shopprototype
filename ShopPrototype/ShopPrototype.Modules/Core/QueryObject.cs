namespace ShopPrototype.Modules.Core
{
	public class QueryObject
	{
		public int PageSize { get; set; }

		public int PagesCount { get; set; }

		public int CurrentPage { get; set; }

		public int? GoToPage { get; set; }

		public int? ItemsTotal { get; set; }

		public string SortBy { get; set; }
	}
}
