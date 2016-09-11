using System;
using ShopPrototype.Modules.Core;

namespace ShopPrototype.DataAccess.EF
{
	public abstract class Repository : IRepository
	{
		protected UnitOfWork UnitOfWork { get; set; }

		public IUnitOfWork BeginUnitOfWork()
		{
			UnitOfWork = new UnitOfWork();

			return UnitOfWork;
		}

		public T GetEntity<T>(object key) where T : class
		{
			return UnitOfWork.Context.Set<T>().Find(key);
		}

		public void AddEntity<T>(T entity) where T : class
		{
			UnitOfWork.Context.Set<T>().Add(entity);
		}
	}
}
