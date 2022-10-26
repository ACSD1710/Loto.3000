using Loto3000.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Loto3000Application.Repository
{
    public interface IRepository<T> where T : IEntity
    {
        T? GetByID (int id);
        IQueryable<T> GetAll();
        T Create (T entity);
        void Update (T entity);
        T Delete (T entity);
        void DeleteAll ();

    }
}
