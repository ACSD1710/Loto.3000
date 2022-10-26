using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000Application.Dto.AdminDto
{
    public class CreateAdminDto
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [Compare("ConfirmPassword", ErrorMessage = "Password doesn't match")]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string ConfirmPassword { get; set; } = string.Empty;

    }
}
