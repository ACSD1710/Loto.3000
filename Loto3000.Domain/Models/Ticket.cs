using Loto3000.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000.Domain.Models
{
    public class Ticket : IEntity
    {
        public Ticket()
        {

        }

        public Ticket(User user, Draw draw, string combinationNumbers)
        {
            Draw = draw;
            User = user;
            DateOfCreateTicket = DateTime.Now;
            CombinationNumbers = combinationNumbers;
            IsActive = true;
            HasPrice = false;
            TicketOwner = user.LastName;
            Prize = TicketPrize.Nothing;
        }
       

        public int Id { get; set; }
        public User? User { get; set; }
        public Draw? Draw { get; set; }
        public DateTime DateOfCreateTicket = DateTime.Now;
        public string CombinationNumbers { get; set; } = string.Empty; 
        public bool IsActive { get; set; } 
        public bool HasPrice { get; set; }
        public TicketPrize Prize { get; set; } = TicketPrize.Nothing; 
        public string TicketOwner = string.Empty;


        //public void CreateCombination(int[] numbs)
        //{
        //    var combination = numbs.Select(x => new Combination(x, this));
        //    foreach(var i in combination)
        //    {
        //        CombinationNumbers.Add(i);
        //    }
        //}
       //public void ValidateCombination(int[] nums)
       // {
       //     IList<Combination> combs = new List<Combination>();

       //     for (int i = 0; i < nums.Length; i++)
       //     {
       //         Combination combination = new Combination();
       //         int enumerator = 0;
                

       //         if (nums[i] < 1 || nums[i] > 37 )
       //             throw new Exception("Number is not valid, please choose a number between 1 and 37");
                
       //         if(nums.Length > 7 || nums.Length < 7)
       //             throw new Exception("You must enter only 7 numbers");
                
       //         var g = Equals(enumerator, nums[i]);

       //         if (enumerator == nums[i])
       //             throw new Exception("You can't duplicate numbers");

       //         enumerator = nums[i];    
       //         combination.Number = nums[i];

       //         combs.Add(combination);
       //     }

       //     CombinationNumbers = combs;
       // }
    }
}