using Loto3000.Domain.Models;
using Loto3000Application.Dto.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000Application.Mapper
{
    public static class UserMapper
    {
        public static UserDto ToUserDtoModel(this User user)
        {
            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                UserName = user.Username,
                Credits = user.Credits
            };
        }
    }
}
