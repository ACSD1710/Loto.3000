using Loto3000.Domain.Models;
using Loto3000Application.Dto.GameDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000Application.Mapper
{
    public static class GameMaper
    {
        public static CreateGameDto StartGameMaper(this Game game)
        {
            return new CreateGameDto
            {
                DateTime = DateTime.Now,
                Numbers = game.GameNumbers,
            };
        }
    }
}
