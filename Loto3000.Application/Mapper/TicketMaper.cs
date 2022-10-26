using Loto3000.Domain.Models;
using Loto3000Application.Dto.NewFolder;
using Loto3000Application.Dto.TicketDto;
using Loto3000Application.Dto.UserDto;
using Loto3000Application.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000Application.Mapper
{
    public static class TicketMaper
    {
        public static TicketDto ToTicketModel(this Ticket ticket)
        {
            return new TicketDto
            {
                Name = ticket.TicketOwner,
                Combination = ticket.CombinationNumbers,
                CreateDate = ticket.DateOfCreateTicket,
                IsActive = ticket.IsActive,
                
            };
        }

        public static WinningTicketDto ToWiinerTicketModel(this Ticket ticket)
        {
            return new WinningTicketDto
            {
                WinnerName = ticket.TicketOwner,
                Combination = ticket.CombinationNumbers,
                Prize = ticket.Prize,

            };
        }
    }
}
