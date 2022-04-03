using Orion.Core;

namespace Orion.Infra.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _databaseContext;
        private bool _disposed = false;

        public UnitOfWork(ApplicationContext ApplicationContext)
        {
            _databaseContext = ApplicationContext;
        }

        public Task Commit()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            return _databaseContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing && _databaseContext != null)
            {
                _databaseContext.Dispose();
            }

            _disposed = true;
        }
    }
}
