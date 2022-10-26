using Loto3000Application.Dto.UserDto;
using Loto3000Application.Exeption;
using Loto3000Application.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Loto3000.Controllers
{
    [Authorize]
    [Route("api/loto/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
      
        private readonly IUserService userServices;

        public UserController(IUserService userServices)
        {
            this.userServices = userServices;
        }

        [AllowAnonymous]
        [HttpPost("createUser")]
        public ActionResult CreateUser(CreateUserDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userModel = userServices.CreateUser(model);
                return Created("api/loto/user/login", userModel);
            }
            catch(ValidationException)
            {
                return BadRequest();
            }
                
        }
        [Authorize(Roles = "User")]
        [HttpPost("change-password")]
        public ActionResult ChangePassword(ChangePasswordDto model)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            if (model is null)
            {
                return BadRequest(model);
            }
            try
            {
                userServices.ChangePassword(model, userId);
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

        [Authorize(Roles = "User")]
        [HttpPost("editUser")]
        public ActionResult EditUser(UpdateUserDto model)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (model is null )
            {
                return BadRequest(model);
            }
            try
            {
                userServices.UpdateUser(model, Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
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
        [Authorize(Roles = "User")]
        [HttpPost("deleteUser")]
        public ActionResult DeleteUser()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            try
            {
                userServices.DeleteUser(userId);
                return Ok();
            }
            catch (NotFoundException)
            {
                return BadRequest();
            }
            
        }
        [Authorize(Roles = "User")]
        [HttpGet("userInfo")]
        public ActionResult GetUserInfo()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            try
            {
                var userInfo = userServices.GetUserInfo(userId);
                return Ok(userInfo);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
        [Authorize(Roles = "User")]
        [HttpPost("buycredits")]
        public ActionResult BuyCredits (UserBuyCreditsDto model)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (model is null)
            {
                return BadRequest(model);
            }
            try
            {
                var massage = userServices.BuyCredits(model, userId);
                return Ok(massage);
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
        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult Login(LoginUserDto model)
        {
            try
            {
                var token = userServices.Authenticate(model);
                return Ok(token.Token);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}

