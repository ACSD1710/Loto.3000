
using Loto3000.Domain.Models;
using Loto3000Application.Dto.UserDto;
using Loto3000Application.Exeption;
using Loto3000Application.Mapper;
using Loto3000Application.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Loto3000Application.Services.Implementation
{
    public class UserService : IUserService
        //active drow?
    {
        private readonly IRepository<User> repository;
        private readonly IPasswordHasher passwordHasher;
        private readonly IConfiguration _coniguration;
        private readonly IRepository<Role> roleRepository;
       
        public UserService(IRepository<User> repository, IPasswordHasher passwordHasher,
                               IConfiguration configuration, IRepository<Role> roleRepository)
        {
            this.repository = repository;
            this.passwordHasher = passwordHasher;
            this._coniguration = configuration;
            this.roleRepository = roleRepository;
            
        }
        // Setfice for buying credits


        public UserDto CreateUser(CreateUserDto model)
        {
            var birthDay = model.DateOfBirth.AddYears(18);
            if (birthDay > DateTime.Now)
            {
                throw new ValidationException("You must have 18 years old");
            }
            var password = passwordHasher.HashPassword(model.Password);
            var user = new User(model.FirstName, model.LastName, 
                                model.UserName, password, model.Email, model.DateOfBirth);
            var userRole = roleRepository.GetByID(2);
            userRole!.UserRoles.Add(user);
            user.Roles.Add(userRole!);
            repository.Create(user);
            return user.ToUserDtoModel();
        }                 
        public void UpdateUser(UpdateUserDto model, int id)
        {
           
            var user = repository.GetByID(id);
            if(user is null)
            {
                throw new NotFoundException("User doesn't Exist");
            }
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            if(model.DateOfBirth.AddYears(18) > DateTime.Now)
            {
                throw new ValidationException("You must have 18 years old");
            }
            user.DateOfBirth = model.DateOfBirth;
            repository.Update(user);
        }

        public void DeleteUser(int id)
        {
            var user = repository.GetByID(id);
            if (user == null)
            {
                throw new NotFoundException("User doesn't exist");
            }
            repository.Delete(user);

        }
        public void ChangePassword(ChangePasswordDto model, int id)
        {
            var user = repository.GetByID(id);
            if (user is null)
            {
                throw new NotFoundException("User doesn't Exist");
            }
                     
            if (user.Password == passwordHasher.HashPassword(model.NewPassword))
            {
                throw new ValidationException("Old password cannot be the same as the new one!");
            }
                

            user.Password = passwordHasher.HashPassword(model.NewPassword);
            repository.Update(user);
        }
        public UserDto GetUserInfo(int id)
        {
            var user = repository.GetByID(id);
            if (user is null)
            {
                throw new NotFoundException("User doesn't Exist");
            }
            return user.ToUserDtoModel();
        }
        public string BuyCredits(UserBuyCreditsDto model, int id )
        {
            var user = repository.GetByID(id);
            if (user is null)
            {
                throw new NotFoundException("User doesn't Exist");
            }
            if (model is null)
            {
                throw new ValidationException("you need to fill in all the fields ");
            }
            user.Credits = model.TotalCredits;
            var credits = model.TotalCredits.ToString();
            repository.Update(user);
            var massage = $"Оn your account you have + {credits} Credits";
            return massage;
        }
        public string Login(LoginUserDto model) 
        {
            var user = repository.GetAll().FirstOrDefault(x => x.Username == model.UserName && 
                                                               x.Password == passwordHasher.HashPassword(model.Password)) 
                                                          ?? throw new NotFoundException("Invalid Username or Password!");

            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_coniguration["Jwt:Key"]));
            var credentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim("CustomClaimTypeUsername", user.Username)
            };

            //foreach (var role in user.Roles)
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, role.Name));
            //}


            var token = new JwtSecurityToken(_coniguration["Jwt:Issuer"],
                                             _coniguration["Jwt:Audience"],
                                             claims,
                                             expires: DateTime.Now.AddMinutes(15),
                                             signingCredentials: credentials);

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);


            return tokenString;
        }
        public TokenDto Authenticate(LoginUserDto model)
        {
            var user = repository.GetAll().Include(x => x.Tickets).Include(i => i.Roles)
                .FirstOrDefault(y => y.Username.Equals(model.UserName) &&
                y.Password.Equals(passwordHasher.HashPassword(model.Password)));
           
            if(user == null)
            {
                throw new NotFoundException("User doesn't Exist");
            }

            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_coniguration["Jwt:Key"]));
            var credentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user!.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim("CustomClaimTypeUsername", user.Username)
            };
            
            foreach(var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }
            var token = new JwtSecurityToken(_coniguration["Jwt:Issuer"],
                                             _coniguration["Jwt:Audience"],
                                             claims,
                                             expires: DateTime.Now.AddMinutes(15),
                                             signingCredentials: credentials);
            var tokenDto = new TokenDto()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
            return tokenDto;
        }
    }
}
