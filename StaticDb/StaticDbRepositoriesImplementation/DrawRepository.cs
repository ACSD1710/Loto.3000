using Loto3000.Domain.Models;
using Loto3000Application.Repository;
using StaicDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000.Infrastructure.Repositories
{
    public class DrawRepository : IRepository<Draw>
    {
        public Draw Create(Draw entity)
        {
            var drowId = PublicStaticLotoDb.Draws.LastOrDefault()?.Id ?? 0;
            entity.Id = ++drowId;
            PublicStaticLotoDb.Draws.Add(entity);
            return entity;
        }

        public Draw Delete(Draw entity)
        {
            PublicStaticLotoDb.Draws.Remove(entity);
            return entity;
        }

        public void DeleteAll()
        {
            PublicStaticLotoDb.Draws.Clear();
        }

        public IQueryable<Draw> GetAll()
        {
            return PublicStaticLotoDb.Draws.AsQueryable();
        }

        public Draw? GetByID(int id)
        {
            return PublicStaticLotoDb.Draws.FirstOrDefault(x => x.Id == id);
        }

        public void Update(Draw entity)
        {
            var ticket = GetByID(entity.Id);
            if (ticket != null)
                ticket = entity;
        }
    }
}
