using System;
using System.Linq;
using System.Linq.Expressions;

namespace ShopPrototype.DataAccess.EF
{
	public abstract class SortCommand<T>
	{
		public string Key { get; set; }

		public abstract IQueryable<T> Sort(IQueryable<T> query);
	}

	public class SortCommand<TObject, TProperty> : SortCommand<TObject>
	{
		public Expression<Func<TObject, TProperty>> Selector { get; set; }

		public override IQueryable<TObject> Sort(IQueryable<TObject> query)
		{
			if (Key.ToUpper().EndsWith("ASC"))
			{
				return query.OrderBy(Selector);
			}
			else if (Key.ToUpper().EndsWith("DESC"))
			{
				return query.OrderByDescending(Selector);
			}
			else
			{
				throw new InvalidOperationException("Wrong Key");
			}
		}
	}
}
