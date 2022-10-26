
using System.Collections.Generic;

namespace Loto3000.Domain.Models
{
    public class Draw : IEntity
    {
        public int Id { get; set; }
        public DateTime StartGame { get;  set; }
        public DateTime EndGame { get;  set; }
        public bool IsActive { get; set; }
        public double TotalCredits { get; set; } = 0;
        public Admin Admin { get; set; }

        
        public Draw() { }
        public Draw(Admin admin)
        {
            Admin = admin;
            StartGame = DateTime.Now;
            EndGame = StartGame.AddDays(7);
            IsActive = true;
            TotalCredits = 0;
        }

        

        public bool IsActiveDraw()
        {
            if (DateTime.Now == EndGame)
                return false;
            return true;
        }
        //public Draw(int drawOrderNum, IList<Ticket> tickets, DateTime drawTime, Session session)
        //{
        //    DrawSequenceNumber = drawOrderNum;
        //    Tickets = tickets;
        //    DrawTime = drawTime;
        //    Session = session;
        //    DrawNumbers = DrawNums();
        //}
    }
}
