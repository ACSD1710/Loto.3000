using Loto3000.Domain.Models;
using Loto3000Application.Repository;
using StaicDb;

namespace Loto3000.Infrastructure.Repositories
{
    public class UserRepository : IRepository<User>
    {
        public User Create(User entity)
        {
            var userId = PublicStaticLotoDb.Users.LastOrDefault()?.Id ?? 0;
            entity.Id = ++userId;
            PublicStaticLotoDb.Users.Add(entity);
            return entity;
        }

        public User Delete(User entity)
        {
            PublicStaticLotoDb.Users.Remove(entity);
            return entity;
        }

        public void DeleteAll()
        {
            PublicStaticLotoDb.Users.Clear();
        }

        public IQueryable<User> GetAll()
        {
            return PublicStaticLotoDb.Users.AsQueryable();
        }

        public User? GetByID(int id)
        {
            return PublicStaticLotoDb.Users.FirstOrDefault(x => x.Id == id);
        }

        public void Update(User entity)
        {
            var user = GetByID(entity.Id);
            if (user != null)
                user = entity;

        }
    }
}
