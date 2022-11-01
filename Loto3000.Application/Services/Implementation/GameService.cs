using Loto3000.Domain.Enums;
using Loto3000.Domain.Helprers;
using Loto3000.Domain.Models;

using Loto3000Application.Dto.GameDto;
using Loto3000Application.Dto.TicketDto;
using Loto3000Application.Exeption;
using Loto3000Application.Mapper;
using Loto3000Application.Repository;

namespace Loto3000Application.Services.Implementation
{
    public class GameService : IGameService
    {
        private readonly IRepository<Admin> adminRepository;
        private readonly IRepository<Draw> drawRepository;
        private readonly IRepository<Game> gameRepository;
        private readonly IRepository<Ticket> ticketRepository;
        

        public GameService(IRepository<Admin> adminRepository, IRepository<Draw> drawRepository,
                                            IRepository<Game> gameRepository, IRepository<Ticket> ticketRepository)
        {
            this.adminRepository = adminRepository;
            this.drawRepository = drawRepository;
            this.gameRepository = gameRepository;
            this.ticketRepository = ticketRepository;
        }

        public CreateGameDto CreateGameFromAdmin(int adminId)
        {
            var admin = adminRepository.GetByID(adminId);
            if (admin is null)
            {
                throw new NotFoundException("Administrator doesn't Exist");
            };

            var drawActive = drawRepository.GetAll().FirstOrDefault(x => x.IsActive == true);
           
            if (drawActive == null)
            {
                throw new ArgumentLotoExeption("There is no already an active draw!");
            }

            //if(DateTime.Now <= drawActive.EndGame)
            //{
            //    throw new ArgumentLotoExeption("The drow is not active please try letter");
            //}
            
            var gameActive = gameRepository.GetAll().FirstOrDefault(x => x.IsActive == true);
           
            if (gameActive != null)
            {
                throw new ArgumentLotoExeption("There is already an active Game!");
            }

            var game = new Game(drawActive, admin);
            admin.Game.Add(game);  
            gameRepository.Create(game);
            adminRepository.Update(admin);
            return game.StartGameMaper();

        }

        public IEnumerable<WinningTicketDto> WinningPrizesFromUsers(int adminId)
        {
            var admin = adminRepository.GetByID(adminId);
            if (admin is null)
            {
                throw new NotFoundException("Administrator doesn't Exist");
            };
            var gameActive = gameRepository.GetAll().FirstOrDefault(x => x.IsActive == true);
            var drawActive = drawRepository.GetAll().FirstOrDefault(x => x.IsActive == true);

            //if (gameActive != null)
            //{
            //    throw new ArgumentLotoExeption("There is already an active Game!");
            //}

            var tickets = ticketRepository.GetAll().ToList();

            var ticketCombinations = tickets.Select(x => x.CombinationNumbers.StringToIntList()).ToList();

            var gameNumbers = gameRepository.GetAll().FirstOrDefault(x => x.IsActive == true)!.GameNumbers;

            var gameCombinations = gameNumbers.StringToIntList().ToList();

            foreach (var ticket in tickets)
            {
                
                int iterator = 0;
                var combination = ticket.CombinationNumbers.StringToIntList();
                foreach(var gcom in gameCombinations)
                {
                    foreach(var tcom in combination)
                    {
                        if(tcom == gcom)
                        {
                            ++iterator;
                        }
                    }

                }
                switch (iterator)
                {
                    case 3:
                        ticket.HasPrice = true;
                        ticket.Prize = TicketPrize.GiftCard_50;
                        
                        break;
                    case 4:
                        ticket.HasPrice = true;
                        ticket.Prize = TicketPrize.GiftCard_100;
                        
                        break;
                    case 5:
                        ticket.HasPrice = true;
                        ticket.Prize = TicketPrize.TV;
                        
                        break;
                    case 6:
                        ticket.HasPrice = true;
                        ticket.Prize = TicketPrize.Vacation;
                        
                        break;
                    case 7:
                        ticket.HasPrice = true;
                        ticket.Prize = TicketPrize.Car;
                        
                        break;

                }
                
                ticketRepository.Update(ticket);
            }
            gameActive!.IsActive = false;
            gameRepository.Update(gameActive);
            drawActive!.IsActive = false;
            drawRepository.Update(drawActive);

            var wintickets = ticketRepository.GetAll().Where(x => x.IsActive == true)
                                    .Where(y => y.HasPrice == true).ToList();
            var winnigTickets = wintickets.Select(z => z.ToWiinerTicketModel()).ToList();
            foreach (var ticket in tickets)
            {
                ticket.IsActive = false;
                ticketRepository.Update(ticket);
            }
            return winnigTickets;
        }
    }
}
