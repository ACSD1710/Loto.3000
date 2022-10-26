
using Loto3000Application.Dto.AdminDto;
using Loto3000Application.Dto.TicketDto;
using Loto3000Application.Exeption;
using Loto3000Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Loto3000.Controllers
{
    [Authorize]
    [Route("api/loto/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService gameService;
        public GameController(IGameService gameService)
        {
            this.gameService = gameService;
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost("create-game")]
        public ActionResult CreateGame()
        {
            int adminId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            try
            {
                var game = gameService.CreateGame(adminId);
                return Created("api/loto/game/createdGame", game);  
            }
            catch(NotFoundException)
            {
                return NotFound();  
            }
            catch (ValidationException)
            {
                return BadRequest();
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpGet("winners")]
        public ActionResult<IEnumerable<WinningTicketDto>> GamePrizes()
        {
            int adminId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            try
            {
                var prizes = gameService.Prizes(adminId);
                return prizes.ToList();
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
    }
}
