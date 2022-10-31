using Loto3000.Domain.Helprers;
using Loto3000.Domain.Models;
using Loto3000Application.Dto.NewFolder;
using Loto3000Application.Dto.TicketDto;
using Loto3000Application.Exeption;
using Loto3000Application.Mapper;
using Loto3000Application.Repository;

namespace Loto3000Application.Services.Implementation
{
    public class TicketService : ITicketService
    {
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Admin> adminRepository;
        private readonly IRepository<Ticket> ticketRepository;
        private readonly IRepository<Draw> drawRepository;
        
        public TicketService(IRepository<User> userRepository, IRepository<Ticket> ticketRepository,
                                                                   IRepository<Draw> drawRepository,
                                                                IRepository<Admin> adminRepository)
        {
            this.userRepository = userRepository;
            this.ticketRepository = ticketRepository;
            
            this.drawRepository = drawRepository;
            this.adminRepository = adminRepository;
        }

        public TicketDto CreateTicket(CreateCombinationModel combination, int userId)
        {
            var user = userRepository.GetByID(userId);
            if (user == null)
            {
                throw new NotFoundException("User doesn't exist");
            }
            var draw = drawRepository.GetAll().FirstOrDefault(x => x.IsActive == true);
            
            if(draw == null)
            {
                throw new NotFoundException("There is not active Draw");
            }

            var combinationArray = combination.Combination!.ValidateCombination();
            var inputCombination = combinationArray.IntListToString();

            Ticket ticket = new Ticket(user, draw, inputCombination);
         
            ticketRepository.Create(ticket);
            if(user.Credits < 5)
            {
                throw new ArgumentLotoExeption("You don't have enought credit");
            }
            user.Credits -= 5;
            draw.TotalCredits += 5;
            user.Tickets.Add(ticket);
            drawRepository.Update(draw);
            userRepository.Update(user);                    
            return ticket.ToTicketModel();
            
        }    

      public IEnumerable<TicketDto> GetAll(int adminId)
        {
            var admin = adminRepository.GetByID(adminId);            
            if (admin == null)
            {
                throw new NotFoundException("Administrator doesn't exist");
            }
            return ticketRepository.GetAll().Select(x => x.ToTicketModel()).ToList();
        }

      public IEnumerable<TicketDto> GetAllActive(int adminId)
        {
            var admin = userRepository.GetByID(adminId);
            if (admin == null)
            {
                throw new NotFoundException("Administrator doesn't exist");
            }
            return ticketRepository.GetAll().Where(y => y.IsActive == true).Select(x => x.ToTicketModel()).ToList();
        }

          
    }
}
