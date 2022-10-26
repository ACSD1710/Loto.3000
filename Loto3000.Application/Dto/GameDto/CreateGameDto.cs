using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000Application.Dto.GameDto
{
    public class CreateGameDto
    {
        public DateTime DateTime { get; set; }
        public string Numbers { get; set; } = string.Empty;
    }
}
