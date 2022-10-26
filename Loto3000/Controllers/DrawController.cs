using Loto3000Application.Dto.AdminDto;
using Loto3000Application.Dto.DrawDto;
using Loto3000Application.Exeption;
using Loto3000Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;
using System.Security.Claims;

namespace Loto3000.Controllers
{
    [Authorize]
    [Route("api/lotto/[controller]")]
    [ApiController]
    public class DrawController : ControllerBase
    {
        private readonly IDrawService drawService;
        public DrawController(IDrawService drawService)
        {
            this.drawService = drawService;
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("create-drow")]
        public ActionResult CreateDrow()
        {
            int adminId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            try
            {
                var draw = drawService.CreateDrow(adminId);
                return Created("api/lotto/draw/createDrow", draw);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            
            
        }
        [Authorize(Roles = "Administrator")]
        [HttpGet("get-all")]
        public ActionResult GetAll()
        {
            int adminId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            try
            {
                var draws = drawService.GetAll(adminId);
                return Ok(draws);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

        }
        [Authorize(Roles = "Administrator")]
        [HttpPost("deletedrow")]
        public ActionResult DeleteDrow ()
        {
            int adminId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            try
            {
                drawService.DeleteActiveDrow(adminId);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            
        }
       
    }
}
