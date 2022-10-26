using Loto3000.Domain.Models;
using Loto3000Application.Dto.DrawDto;
using Loto3000Application.Dto.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000Application.Mapper
{
    public static class DtoMapper
    {
        public static DrowDto ToDrawDtoModel(this Draw draw)
        {
            return new DrowDto
            {
                //Creator = draw.Admin.Name,
                StartGame = draw.StartGame,
                EndGame = draw.EndGame, 
            };
        }
    }
}
