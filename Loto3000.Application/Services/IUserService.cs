using Loto3000Application.Dto.UserDto;


namespace Loto3000Application.Services
{
    public interface IUserService
    {
        public UserDto CreateUser(CreateUserDto user);
        public void ChangePassword(ChangePasswordDto model, int id);
        public void DeleteUser(int id);
        public void UpdateUser(UpdateUserDto model, int id);
        public UserDto GetUserInfo(int id);
        public string BuyCredits(UserBuyCreditsDto model, int id);
        public string Login(LoginUserDto model);
        public TokenDto Authenticate(LoginUserDto model);
    }
}
