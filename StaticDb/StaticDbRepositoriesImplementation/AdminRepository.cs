using Loto3000.Domain.Models;
using Loto3000Application.Repository;
using StaicDb;

namespace Loto3000.Infrastructure.Repositories
{
    public class AdminRepository : IRepository<Admin>
    {
        public Admin Create(Admin admin)
        {
            var id = PublicStaticLotoDb.Admins.LastOrDefault()?.Id ?? 0;
            admin.Id = ++id;
            PublicStaticLotoDb.Admins.Add(admin);
            return admin;
        }

        public Admin Delete(Admin admin)
        {
            PublicStaticLotoDb.Admins.Remove(admin);
            return admin;
        }

        public void DeleteAll()
        {
            PublicStaticLotoDb.Admins.Clear();
        }

        public IQueryable<Admin> GetAll()
        {
            return PublicStaticLotoDb.Admins.AsQueryable();
        }

        public Admin? GetByID(int id)
        {
            return PublicStaticLotoDb.Admins.FirstOrDefault(x => x.Id == id);
        }

        public void Update(Admin admin)
        {
            var entity = GetByID(admin.Id);
            if (entity != null)
                entity = admin;
        }
    }
}
