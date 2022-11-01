using Loto3000.Domain.Models;
using Loto3000Application.Dto.AdminDto;
using Loto3000Application.Dto.UserDto;
using Loto3000Application.Exeption;
using Loto3000Application.Services;
using Loto3000Application.Services.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace Loto3000.Controllers
{
    [Authorize]
    [Route("api/loto/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService adminServices;
        private readonly Serilog.ILogger logger;
        public AdminController(IAdminService adminServices, Serilog.ILogger logger)
        {
            this.adminServices = adminServices;
            this.logger = logger;
            logger.Debug("");
            logger.Information("");
            logger.Warning("");
            logger.Error("");
        }


        [Authorize(Roles = "Administrator")]
        [HttpPost("createAdmin")]
        public ActionResult CreateAdmin(CreateAdminDto model)
        {
            if (!ModelState.IsValid)
            {
                logger.Error("Information doesnt match");
                return BadRequest(ModelState);
            }
            var adminModel = adminServices.CreateAdmin(model);
            return Created("api/loto/admin/register", adminModel);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("change-password")]
        public ActionResult ChangePassword(ChangePassAdmin model)
        {
            int adminId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            try
            {
                adminServices.ChangeAdminPassword(model, adminId);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (ValidationException)
            {
                return BadRequest();
            }


        }
        [Authorize(Roles = "Administrator")]
        [HttpPost("DeleteAdmin")]
        public ActionResult DeleteAdmin( )
        {
            int adminId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            try
            {
                adminServices.DeleteAdmin(adminId);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                logger.Warning($"Its something wrong for {adminId} admin {new ClaimsPrincipalWrapper(User).Id}", ex);
                return NotFound();
            }
           
        }
        [AllowAnonymous]
        [HttpPost("admin-login")]
        public ActionResult Login(AdminLoginDto model)
        {
            try
            {
                var token = adminServices.AuthenticateAdmin(model);
                return Ok(token.Token);
            }
            catch (NotFoundException)
            {
                logger.Debug("information doesnt match");
                return NotFound();
            }
        }
    }
   
}
