using Loto3000.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000Application.Dto.TicketDto
{
    public class WinningTicketDto
    {
        public string WinnerName { get; set; } = string.Empty;
        public  TicketPrize Prize { get; set; }
        public string Combination { get; set; } = string.Empty;
    }
}
