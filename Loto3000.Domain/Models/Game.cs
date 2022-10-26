using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000.Domain.Models
{
    public class Game : IEntity
    {
        public int Id { get; set; }
        public Draw? Draw { get; set; }
        public string GameNumbers { get; set; } = string.Empty; 
        public bool IsActive { get; set; }
        public Admin Admin { get; set; }

        public Game() { }

        public Game(Draw? draw, Admin admin)
        {
            Draw = draw;
            GameNumbers = DrawNums();
            IsActive = true;
            Admin = admin;
        }

        public string DrawNums()
        {
            Random random = new Random();
            int[] winningNums = Enumerable.Range(1, 37)
                                .OrderBy(x => random.Next())
                                .Take(8)
                                .ToArray();
            string numbers = "";
            for (int i = 0; i < winningNums.Length; i++)
            {
                if (i == 7)
                {
                    numbers += winningNums[i].ToString();
                    break;
                }
                numbers += winningNums[i].ToString() + ",";

            }
            return numbers;
        }
    }
}
