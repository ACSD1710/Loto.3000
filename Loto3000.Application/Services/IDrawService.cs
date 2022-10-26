using Loto3000.Domain.Models;
using Loto3000Application.Dto.AdminDto;
using Loto3000Application.Dto.DrawDto;
using Loto3000Application.Dto.TicketDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000Application.Services
{
    public interface IDrawService
    {
        DrowDto CreateDrow(int id);
        IEnumerable<DrowDto> GetAll(int id);
        DrowDto DeleteActiveDrow(int id);
        
    }
}
