using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TB.Kutuphane.Data.Repository
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        List<T> GetAll(Expression<Func<T, bool>> predicate);
        T GetById(int id);
        T Get(Expression<Func<T, bool>> predicate);

        T Add(T entity);
        T Update(T entity);
        T Delete(T entity);
        void Delete(int id);
    }
}
