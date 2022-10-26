
using Loto3000.Domain.Models;

namespace Loto3000Application.Dto.NewFolder
{
    public class TicketDto
    {
        public string Name { get; set; } = string.Empty;    
        public string Combination { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }
    }
}
