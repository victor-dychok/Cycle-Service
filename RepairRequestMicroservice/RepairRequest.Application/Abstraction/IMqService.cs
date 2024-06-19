using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairRequest.Application.Abstraction
{
    public interface IMqService
    {
        public void SendMessage(string queue, string message);
    }
}
