using Loto3000.Domain.Models;
using Loto3000Application.Dto.AdminDto;
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
    public class AdminService : IAdminService
    {
        private readonly IRepository<Admin> repository;
        private readonly IPasswordHasher passwordHasher;
        private readonly IRepository<Role> roleRepository;
        private readonly IConfiguration configuration;
        public AdminService(IRepository<Admin> repository, IPasswordHasher passwordHasher, IRepository<Role> roleRepository, IConfiguration configuration)
        {
            this.repository = repository;
            this.passwordHasher = passwordHasher;
            this.roleRepository = roleRepository;
            this.configuration = configuration;
        }
     
        public AdminDto CreateAdmin(CreateAdminDto model)
        {
            var password = passwordHasher.HashPassword(model.Password);
            var admin = new Admin(model.FirstName, password);
           
            var adminRole = roleRepository.GetByID(1);
            adminRole!.AdminRoles.Add(admin);
            admin.Roles.Add(adminRole);
            
            repository.Create(admin);
            return admin.ToAdminDtoMode();
        }

    
        public void ChangePassword(ChangePassAdmin model, int id)
        {
            var admin = repository.GetByID(id);
            if (admin is null)
            {
                throw new NotFoundException("Administrator doesn't Exist");
            };

            if (passwordHasher.HashPassword(admin.Password) == model.NewPassword)
            {
                throw new ValidationException("New Password and Old Password must be defferent");
            }
           

            admin.Password = passwordHasher.HashPassword(model.NewPassword);  
            repository.Update(admin);
        }
        
        
        public void DeleteAdmin(int id)
        {
            var admin = repository.GetByID(id);
            if (admin is null)
            {
                throw new NotFoundException("Administrator doesn't Exist");
            };
            
            repository.Delete(admin);
        }
        public TokenDto Authenticate(AdminLoginDto adminDto)
        {
            var admin = repository.GetAll().Include(i => i.Game).Include(y => y.Draw).Include(z => z.Roles)
                .FirstOrDefault(x =>
                         x.Username == adminDto.UserName && x.Password == passwordHasher.HashPassword(adminDto.Password));

            if (admin == null)
            {
                throw new NotFoundException("User doesn't Exist");
            }

            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, admin!.Id.ToString()),
                new Claim(ClaimTypes.Name, admin.Name),
                new Claim("CustomClaimTypeUsername", admin.Username)
            };

            foreach (var role in admin.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }
            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                                             configuration["Jwt:Audience"],
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
