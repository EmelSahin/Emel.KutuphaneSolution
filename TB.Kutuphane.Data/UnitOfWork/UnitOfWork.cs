using System;
using TB.Kutuphane.Data.Repository;
using TB.Kutuphane.Entity;

namespace TB.Kutuphane.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _databaseContext;
        private bool disposed = false;

        public UnitOfWork()
        {
            _databaseContext = new DatabaseContext();
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return new Repository<T>(_databaseContext);
        }

        public int SaveChanges()
        {
            try
            {
                return _databaseContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _databaseContext.Dispose();
                }
            }
            this.disposed = true;
        }
    }
}