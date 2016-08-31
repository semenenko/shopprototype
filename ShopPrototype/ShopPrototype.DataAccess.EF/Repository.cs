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
	}
}
