
using Loto3000Application.Dto.AdminDto;
using Loto3000Application.Dto.NewFolder;
using Loto3000Application.Dto.TicketDto;
using Loto3000Application.Dto.UserDto;
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
    public class TicketController : ControllerBase
    {
        private readonly ITicketService ticketService;
        public TicketController(ITicketService ticketService)
        {
            this.ticketService = ticketService;
        }

        [Authorize(Roles = "User")]
        [HttpPost("createTicket")]
        public ActionResult CreateTicket(CreateCombinationModel combination)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            try 
            {

                var ticket = ticketService.CreateTicket(combination, userId);
                return Created("api/loto/ticket/createdTicket", ticket);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            
        }
        [Authorize(Roles = "Administrator")]
        [HttpGet("getall")]
        public ActionResult GetAllUserTickets()
        {
            int adminId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            try
            {
                var tickets = ticketService.GetAll(adminId).ToList();
                return Ok(tickets);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpGet("get-all-user-activeTickets")]
        public ActionResult GetAllUserActiveTickets()
        {
            int adminId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            try
            {
                var tickets = ticketService.GetAllActive(adminId);
                return Ok(tickets);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
       

        
    }   
}
