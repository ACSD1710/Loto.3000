using System.ComponentModel.DataAnnotations;


namespace Loto3000Application.Dto.UserDto
{
    public class ChangePasswordDto
    {
  
        [Required]
        [Compare("ConfirmNewPassword", ErrorMessage = "Password doesn't match")]
        //[RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")]
        public string NewPassword { get; set; } = string.Empty;
        [Required]
        public string ConfirmNewPassword { get; set; } = string.Empty;

    }
}
