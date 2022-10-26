

using Loto3000.Domain.Models;
using Loto3000Application.Dto.UserDto;
using System.ComponentModel.DataAnnotations;

namespace Loto3000Application.Dto.TicketDto
{
    public class CreateCombinationModel
    {
        [MinLength(7)]
        [MaxLength(7)]
        public int[]? Combination { get; set; }  
    }
}
