
namespace Loto3000Application.Services
{
    public interface IPasswordHasher
    {
        public string HashPassword(string password);
    }
}
