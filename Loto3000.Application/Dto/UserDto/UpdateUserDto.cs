using System.ComponentModel.DataAnnotations;

namespace Loto3000Application.Dto.UserDto
{
    public class UpdateUserDto
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}
