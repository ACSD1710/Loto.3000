using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000Application.Dto.AdminDto
{
    public class AdminDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string UserName { get; set; } = string.Empty;    

    }
}
