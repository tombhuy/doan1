using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProvider.Model;
using System.Data.Entity;
using PagedList;

namespace DataProvider.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected DbQuanLyThuVienContext _dbContext;
        private readonly IDbSet<T> dbSet;

        public BaseRepository()
        {
            _dbContext = new DbQuanLyThuVienContext();
            dbSet = _dbContext.Set<T>();
        }

        public void Create(T entity)
        {
            dbSet.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            dbSet.Attach(entity);
            dbSet.Remove(entity);
            _dbContext.SaveChanges();
        }

        public T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public void Update(T entity)
        {
            dbSet.Attach(entity);
            _dbContext.Entry(entity).State=EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public IQueryable<T> GetAll(string[] includes = null)
        {
            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0) {
                var query = _dbContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.AsQueryable();
            }

            return _dbContext.Set<T>().AsQueryable();
        }

    }
}
