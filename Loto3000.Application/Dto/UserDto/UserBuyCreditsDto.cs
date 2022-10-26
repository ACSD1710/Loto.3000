using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000Application.Dto.UserDto
{
    public class UserBuyCreditsDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public int CardNumbers { get; set; }
        [Required]
        public int TotalCredits { get; set; }

    }
}
