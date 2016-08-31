using System;

namespace ShopPrototype.Modules.Core
{
	public interface IUnitOfWork : IDisposable
	{
		void Commit();
	}
}
