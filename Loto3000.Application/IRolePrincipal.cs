using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000.Application
{
    public interface IRolePrincipal
    {
        public string Name { get;} 
        public int Id { get;}
        public bool IsInRole(string roleName);
    }
}
