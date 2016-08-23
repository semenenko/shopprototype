namespace ShopPrototype.Modules.Core
{
	public interface IRepository
	{
		IUnitOfWork BeginUnitOfWork();
	}
}
