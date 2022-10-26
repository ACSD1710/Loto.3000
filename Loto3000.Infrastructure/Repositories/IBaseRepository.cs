using Loto3000.Domain.Models;

namespace Loto3000.Infrastructure.Repositories
{
    public interface IBaseRepository<T> where T : class, IEntity
    {
        T Create(T entity);
        T Delete(T entity);
        void DeleteAll();
        IQueryable<T> GetAll();
        T? GetByID(int id);
        void Update(T entity);
    }
}