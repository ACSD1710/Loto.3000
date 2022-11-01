
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
        private readonly Serilog.ILogger logger;
        public TicketController(ITicketService ticketService, Serilog.ILogger logger)
        {
            this.ticketService = ticketService;
            this.logger = logger;
            logger.Debug("");
            logger.Information("");
            logger.Warning("");
            logger.Error("");
        }

        [Authorize(Roles = "User")]
        [HttpPost("createTicket")]
        public ActionResult CreateTicket(CreateCombinationModel combination)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            try 
            {

                var ticket = ticketService.CreateTicketFromUser(combination, userId);
                return Created("api/loto/ticket/createdTicket", ticket);
            }
            catch (NotFoundException ex)
            {
                logger.Warning($"Ticket something not found", ex);
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
                var tickets = ticketService.GetAllTicketFromAdmin(adminId).ToList();
                return Ok(tickets);
            }
            catch (NotFoundException ex)
            {
                logger.Warning($"Its something wrong for {adminId} admin {new ClaimsPrincipalWrapper(User).Id}", ex);
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
                var tickets = ticketService.GetAllActiveTicketsFromAdmin(adminId);
                return Ok(tickets);
            }
            catch (NotFoundException ex)
            {
                logger.Warning($"Its something wrong for {adminId} admin {new ClaimsPrincipalWrapper(User).Id}", ex);
                return NotFound();
            }
        }
       

        
    }   
}
