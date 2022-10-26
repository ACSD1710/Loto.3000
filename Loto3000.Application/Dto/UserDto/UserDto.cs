namespace Loto3000Application.Dto.UserDto
{
    public class UserDto
    {
        
        public int Id { get; set; } 
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public string UserName { get; set; } = string.Empty;
        public double Credits { get; set; }    
        public DateTime DateOfBirth { get; set; }

    }
}
