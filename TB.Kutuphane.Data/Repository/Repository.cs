using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using TB.Kutuphane.Entity;

namespace TB.Kutuphane.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DatabaseContext _databaesContext;
        private readonly DbSet<T> _dbset;

        public Repository(DatabaseContext databaseContext)
        {
            this._databaesContext = databaseContext;
            this._dbset = databaseContext.Set<T>();
        }

        public T Add(T entity)
        {
            return _dbset.Add(entity);
        }

        public T Delete(T entity)
        {
            return _dbset.Remove(entity);
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity == null) return;
            Delete(entity);
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return _dbset.Where(predicate).FirstOrDefault();
        }

        public List<T> GetAll()
        {
            return _dbset.ToList();
        }

        public List<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return _dbset.Where(predicate).ToList();
        }

        public T GetById(int id)
        {
            return _dbset.Find(id);
        }

        public T Update(T entity)
        {
            _dbset.Attach(entity);
            _databaesContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }
    }
}
