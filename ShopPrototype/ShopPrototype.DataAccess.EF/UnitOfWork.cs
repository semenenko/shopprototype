using ShopPrototype.Modules.Core;

namespace ShopPrototype.DataAccess.EF
{
	public class UnitOfWork : IUnitOfWork
	{
		public UnitOfWork()
		{
			Context = new Context();
		}

		public Context Context { get; private set; }

		public void Commit()
		{
			if (Context != null)
				Context.SaveChanges();
		}

		public void Dispose()
		{
			if (Context != null)
				Context.Dispose();
		}
	}
}
