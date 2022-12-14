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
        private readonly Serilog.ILogger logger;
        public DrawController(IDrawService drawService, Serilog.ILogger logger)
        {
            this.drawService = drawService;
            this.logger = logger;
            logger.Debug("");
            logger.Information("");
            logger.Warning("");
            logger.Error("");
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("create-drow")]
        public ActionResult CreateDrow()
        {
            int adminId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            try
            {
                var draw = drawService.CreateDrowFromAdmin(adminId);
                return Created("api/lotto/draw/createDrow", draw);
            }
            catch (NotFoundException ex)
            {
                logger.Warning($"Its something wrong for {adminId} admin {new ClaimsPrincipalWrapper(User).Id}", ex);
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
                var draws = drawService.GetAllDrow(adminId);
                return Ok(draws);
            }
            catch (NotFoundException ex)
            {
                logger.Warning($"Its something wrong for {adminId} admin {new ClaimsPrincipalWrapper(User).Id}", ex);
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
            catch (NotFoundException ex)
            {
                logger.Warning($"Its something wrong for {adminId} admin {new ClaimsPrincipalWrapper(User).Id}", ex);
                return NotFound();
            }
            
        }
       
    }
}
