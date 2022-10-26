using System;
using System.Collections.Generic;
using Loto3000Application.Dto.AdminDto;
using Loto3000Application.Dto.NewFolder;
using Loto3000Application.Dto.TicketDto;
using Loto3000Application.Dto.UserDto;

namespace Loto3000Application.Services
{
    public interface ITicketService
    {
         
        public TicketDto CreateTicket(CreateCombinationModel combination, int userId);
        public IEnumerable<TicketDto> GetAll(int adminId);
        public IEnumerable<TicketDto> GetAllActive(int adminId);

    }
}
