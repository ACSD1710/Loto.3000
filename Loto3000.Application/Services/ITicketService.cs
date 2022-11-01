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
         
        public TicketDto CreateTicketFromUser(CreateCombinationModel combination, int userId);
        public IEnumerable<TicketDto> GetAllTicketFromAdmin(int adminId);
        public IEnumerable<TicketDto> GetAllActiveTicketsFromAdmin(int adminId);

    }
}
