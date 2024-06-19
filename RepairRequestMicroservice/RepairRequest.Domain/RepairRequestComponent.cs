using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairRequest.Domain
{
    public class RepairRequestComponent
    {
        public int Id { get; set; }
        public int RepairRequestId { get; set; }
        public int ComponentId { get; set; }
        public virtual RepairRequestEntity RepairRequest { get; set; }
        public virtual Component Component { get; set; }
    }
}
