using System.Security.Cryptography;
using Loto3000Application.Services;
using System.Text;

namespace Loto3000.Infrastructure
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            var data = ASCIIEncoding.ASCII.GetBytes(password);
            var shaProvider = new SHA1CryptoServiceProvider();
            return ASCIIEncoding.ASCII.GetString(shaProvider.ComputeHash(data));
        }
    }
}
