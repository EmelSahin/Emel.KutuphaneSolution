using System;
using TB.Kutuphane.Data.Repository;

namespace TB.Kutuphane.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class;
        int SaveChanges();
    }
}
