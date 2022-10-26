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
    public class TicketRepository : IRepository<Ticket>
    {
        public Ticket Create(Ticket entity)
        {
            var ticketId = PublicStaticLotoDb.Tickets.LastOrDefault()?.Id ?? 0;
            entity.Id = ++ticketId;
            PublicStaticLotoDb.Tickets.Add(entity);
            return entity;
        }

        public Ticket Delete(Ticket entity)
        {
            PublicStaticLotoDb.Tickets.Remove(entity);
            return entity;
        }

        public void DeleteAll()
        {
            PublicStaticLotoDb.Tickets.Clear();
        }

        public IQueryable<Ticket> GetAll()
        {
            return PublicStaticLotoDb.Tickets.AsQueryable();
        }

        public Ticket? GetByID(int id)
        {
            return PublicStaticLotoDb.Tickets.FirstOrDefault(x => x.Id == id);
        }

        public void Update(Ticket entity)
        {
            var ticket = GetByID(entity.Id);
            if (ticket != null)
                ticket = entity;
        }
    }
}
