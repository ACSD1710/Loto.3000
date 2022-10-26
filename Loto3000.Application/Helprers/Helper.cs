using Loto3000Application.Exeption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000.Domain.Helprers
{
    public static class Helper
    {
        public static List<int> StringToIntList(this string numbers)
        {
            List<int> numss = numbers.Split(',').Select(x => int.Parse(x)).ToList();
            return numss;
        }
        public static string IntListToString(this  int[] list)
        {
            string numbers = "";
            for (int i = 0; i < list.Length; i++)
            {
                if (i == 6)
                {
                    numbers += list[i].ToString();
                    break;
                }
                numbers += list[i].ToString() + ",";

            }
            return numbers; 
        }
        public static int[] ValidateCombination(this int[] nums)
        {
           for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] < 1 || nums[i] > 37)
                {
                    throw new ValidationException("Number is not valid, please choose a number between 1 and 37");
                }
                    
                if (nums.Length > 7 || nums.Length < 7)
                {
                    throw new ValidationException("You must enter only 7 numbers");
                }
                
            }
            return nums;
        }
    }
}
