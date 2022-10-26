using Loto3000.Domain.Models;
using Loto3000Application.Dto.AdminDto;
using Loto3000Application.Dto.DrawDto;
using Loto3000Application.Dto.NewFolder;
using Loto3000Application.Dto.TicketDto;
using Loto3000Application.Dto.UserDto;
using Loto3000Application.Exeption;
using Loto3000Application.Mapper;
using Loto3000Application.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000Application.Services.Implementation
{
    public class DrawService : IDrawService
    {
        private readonly IRepository<Draw> drowRepository;
        private readonly IRepository<Admin> adminRepository;
        private readonly IPasswordHasher passordHasher;
        public DrawService(IRepository<Draw> drowRepository, IRepository<Admin> adminRepository, IPasswordHasher passordHasher)
        {
            this.drowRepository = drowRepository;
            this.adminRepository = adminRepository;
            this.passordHasher = passordHasher; 
        }

        public DrowDto CreateDrow(int id)
        {
            var admin = adminRepository.GetByID(id) ?? throw new NotFoundException("Admin Doesn't exist");

            var drawActive = drowRepository.GetAll().FirstOrDefault(x => x.IsActive == true);
            // Proverka dali ima aktiven draw
            //var activeDraw = GetActiveDraw();

            // Ako ima  -  Exception
            if (drawActive != null)
                 throw new NotFoundException("There is already an active draw!");

            // Ako nema aktiven draw
            var draw = new Draw(admin);
            
            drowRepository.Create(draw);
            admin.Draw.Add(draw);
            adminRepository.Update(admin);
            return draw.ToDrawDtoModel();         }

        public IEnumerable<DrowDto> GetAll(int id)
        {
            var admin = adminRepository.GetByID(id) ?? throw new NotFoundException("Admin Doesn't exist");
            return drowRepository.GetAll().Select(x => x.ToDrawDtoModel()).ToList();
        }

        public DrowDto DeleteActiveDrow(int id)
        {
            var admin = adminRepository.GetByID(id) ?? throw new NotFoundException("Admin Doesn't exist");

            var drawActive = drowRepository.GetAll().FirstOrDefault(x => x.IsActive == true);

            if (drawActive == null)
                throw new NotFoundException("There is no already an active draw!");
            drowRepository.Delete(drawActive);
            return drawActive.ToDrawDtoModel();
        }

       
        private Draw? GetActiveDraw()
        {
            return drowRepository.GetAll().Where(x => x.StartGame <= DateTime.Now && x.EndGame >= DateTime.Now).FirstOrDefault();
        }
    }
}
