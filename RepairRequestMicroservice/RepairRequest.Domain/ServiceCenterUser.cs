
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairRequest.Domain
{
    public class ServiceCenterUser
    {
        public int Id { get; init; }
        public int UserId { get; set; }
        public int CenterId { get; set; }
    }
}
