using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairRequest.Domain
{
    public class RepairRequestService
    {
        public int Id { get; set; }
        public int RepairRequestId { get; set; }
        public int ServiceId { get; set; }
        public virtual RepairRequestEntity RepairRequest { get; set; }
        public virtual ServiceEntity Service { get; set; }
    }
}
