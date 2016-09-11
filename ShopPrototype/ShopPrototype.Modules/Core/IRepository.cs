namespace ShopPrototype.Modules.Core
{
	public interface IRepository
	{
		IUnitOfWork BeginUnitOfWork();

		T GetEntity<T>(object key) where T : class;

		void AddEntity<T>(T entity) where T : class;
	}
}
