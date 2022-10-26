using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000Application.Exeption
{
    public class ArgumentLotoExeption : Exception
    {
        public ArgumentLotoExeption()
        {
           
        }
        public ArgumentLotoExeption(string? message) : base(message)
        {

        }
        public ArgumentLotoExeption(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
