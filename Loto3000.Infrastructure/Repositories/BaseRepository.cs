

using Loto3000.Domain.Models;
using Loto3000.Infrastructure.EntityFrameWork;
using Loto3000Application.Repository;

namespace Loto3000.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly ApplicationDbContex dbContex;
        public BaseRepository(ApplicationDbContex dbContex)
        {
            this.dbContex = dbContex;
        }
        public T Create(T entity)
        {
            dbContex.Add(entity);
            dbContex.SaveChanges();
            return entity;
        }

        public T Delete(T entity)
        {
            dbContex.Remove(entity);
            dbContex.SaveChanges();
            return entity;
        }

        public void DeleteAll()
        {
           var entity = GetAll().ToList();
            foreach(var item in entity)
            {
                dbContex.Remove(item);
            }
            dbContex.SaveChanges();
        }

        public IQueryable<T> GetAll()
        {
           return dbContex.Set<T>();
        }

        public T? GetByID(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public void Update(T entity)
        {
            dbContex.Update(entity);
            dbContex.SaveChanges();
            
        }
    }
}
