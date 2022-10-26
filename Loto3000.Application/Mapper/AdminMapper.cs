using Loto3000.Domain.Models;
using Loto3000Application.Dto.AdminDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000Application.Mapper
{
    public static class AdminMapper
    {
        public static AdminDto ToAdminDtoMode(this Admin admin)
        {
            return new AdminDto
            {
                Name = admin.Name,
                UserName = admin.Username
            };
        }
    }
}
