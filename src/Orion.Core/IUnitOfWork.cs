namespace Orion.Core
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
