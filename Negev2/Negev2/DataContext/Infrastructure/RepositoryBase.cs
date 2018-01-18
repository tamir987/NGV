using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Negev2.DataContext.Infrastructure
{
    public abstract class RepositoryBase<T> where T : class, new()
    {
        private CropsDb dataContext;
        private readonly IDbSet<T> dbset;
        protected RepositoryBase()
        {
            dataContext = DataContext;
            dbset = dataContext.Set<T>();
        }

        protected CropsDb DataContext
        {
            get { return DatabaseFactory.Get(); }
        }

        public virtual void Save()
        {
            DatabaseFactory.Save();
        }

        public virtual void Dispose()
        {
            DatabaseFactory.Dispose();
        }

        public virtual T Add(T entity)
        {
            dbset.Add(entity);
            return entity;
        }
        public virtual void Update(T entity)
        {
            dbset.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
        }
        public virtual void Delete(T entity)
        {
            dbset.Remove(entity);
        }
        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbset.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbset.Remove(obj);
        }
        public virtual T GetById(int id)
        {
            return dbset.Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return dbset.ToList();
        }
        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).ToList();
        }

        public virtual IQueryable<T> Query(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where);
        }
    }
}