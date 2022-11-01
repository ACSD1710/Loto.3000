using Loto3000Application.Dto.AdminDto;
using Loto3000Application.Dto.GameDto;
using Loto3000Application.Dto.TicketDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000Application.Services
{
    public interface IGameService
    {
        public CreateGameDto CreateGameFromAdmin(int adminId);
        public IEnumerable<WinningTicketDto> WinningPrizesFromUsers(int adminId);
    }
}
