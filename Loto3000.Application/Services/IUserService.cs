using Loto3000Application.Dto.UserDto;


namespace Loto3000Application.Services
{
    public interface IUserService
    {
        public UserDto CreateUser(CreateUserDto user);
        public void ChangeUserPassword(ChangePasswordDto model, int id);
        public void DeleteUser(int id);
        public void UpdateUser(UpdateUserDto model, int id);
        public UserDto GetUserInfo(int id);
        public string BuyCreditsFromUser(UserBuyCreditsDto model, int id);
        public string UserLogin(LoginUserDto model);
        public TokenDto AuthenticateUser(LoginUserDto model);
    }
}
