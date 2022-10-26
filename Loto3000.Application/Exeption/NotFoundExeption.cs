﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000Application.Exeption
{
    public class NotFoundException : Exception
    {
        public NotFoundException()
        {

        }
        public NotFoundException(string? message) : base(message)
        {

        }
        public NotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
