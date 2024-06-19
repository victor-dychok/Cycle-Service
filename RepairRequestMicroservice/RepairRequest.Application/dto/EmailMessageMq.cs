using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairRequest.Application.dto
{
    public class EmailMessageMq
    {
        public string Email { get; set; }
        public string Header { get; set; }
        public string Message { get; set; }
    }
}
