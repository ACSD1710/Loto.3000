using Loto3000.Domain.Models;
using System.Reflection.Metadata;

namespace StaicDb
{
    public static class PublicStaticLotoDb
    {
        public static IList<User> Users { get; set; } = new List<User>()
        {
            new User("Bob", "Bobsky", "bobsky123", "1234", "bob@yahoo.com", new DateTime(1987,10,17))
        };
        public static IList<Admin> Admins{ get; set; } = new List<Admin>()
        {
            new Admin("Darko", "123")
        };
        
        public static IList<Ticket> Tickets { get; set; } = new List<Ticket>()
        {
           
        };

        public static IList<Draw> Draws { get; set; } = new List<Draw>();
    }
}
