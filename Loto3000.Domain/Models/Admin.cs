using Loto3000.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000.Domain.Models
{
    public class Admin : IEntity
    {
        public Admin() 
        { 
        }
        public Admin( string name, string pw)
        {
            Name = name;
            Username = $"Admin{name}";
            Password = pw;
            
        }

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public ICollection<Draw> Draw { get; set; } = new List<Draw>();
        public ICollection<Game> Game { get; set; } = new List<Game>();
        public IList<Role> Roles { get; set; } = new List<Role>();






    }
}
