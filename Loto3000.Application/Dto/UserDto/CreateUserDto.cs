using System.ComponentModel.DataAnnotations;


namespace Loto3000Application.Dto.UserDto
{
    public class CreateUserDto
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [Compare("ConfirmPassword", ErrorMessage = "Password doesn't match")]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string ConfirmPassword { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;


    }
}
