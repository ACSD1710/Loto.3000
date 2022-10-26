using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000.Domain.Models
{
    public class Role : IEntity
    {
        public Role() { }
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<User> UserRoles { get; set; } = new List<User>();
        public ICollection<Admin> AdminRoles { get; set; } = new List<Admin>();
    }
}
