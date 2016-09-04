using ShopPrototype.Modules.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopPrototype.DataAccess.EF
{
	public abstract class QueryObjectProcessor<TQueryObject, TQueryInputItem, TQueryOutputItem> where TQueryObject : QueryObject, new()
	{
		public QueryObjectProcessor(TQueryObject queryObject)
		{
			QueryObject = queryObject;
		}

		public TQueryObject QueryObject { get; protected set; }

		protected IQueryable<TQueryInputItem> Query { get; set; }

		public IEnumerable<TQueryOutputItem> Output { get; protected set; }

		public abstract IEnumerable<SortCommand<TQueryInputItem>> SortCommands { get; }

		protected SortCommand<TQueryInputItem> GetCurrentSortCommand()
		{
			SortCommand<TQueryInputItem> result = null;

			if (QueryObject != null && !string.IsNullOrWhiteSpace(QueryObject.SortBy))
				result = SortCommands.FirstOrDefault(c => c.Key.ToUpper() == QueryObject.SortBy.ToUpper());

			return result;
		}

		public IEnumerable<TQueryOutputItem> ProcessQuery(IQueryable<TQueryInputItem> query, bool applyPager = true)
		{
			if (QueryObject == null || QueryObject.PageSize == 0)
				QueryObject = GetDefaultQueryObject();

			Query = query;

			ApplyFilter();
			CalculateTotals();
			ApplySort();
			if (applyPager)
				ApplyPager();
			ApplySelect();

			return Output;
		}

		void CalculateTotals()
		{
			int itemsTotal = Query.Count();
			QueryObject.ItemsTotal = itemsTotal;
			QueryObject.PagesCount = itemsTotal / QueryObject.PageSize;

			if (QueryObject.PageSize * QueryObject.PagesCount < itemsTotal)
				QueryObject.PagesCount++;
		}

		protected abstract TQueryObject GetDefaultQueryObject();

		protected abstract void ApplyFilter();

		protected virtual void ApplyDefaultSort()
		{
		}

		protected void ApplySort()
		{
			if (GetCurrentSortCommand() != null)
				Query = GetCurrentSortCommand().Sort(Query);
			else
				ApplyDefaultSort();
		}

		protected void ApplyPager()
		{
			if (QueryObject.GoToPage.HasValue)
				QueryObject.CurrentPage = QueryObject.GoToPage.Value;

			Query = Query.Skip(QueryObject.PageSize * (QueryObject.CurrentPage - 1))
				.Take(QueryObject.PageSize);
		}

		protected abstract void ApplySelect();
	}
}
