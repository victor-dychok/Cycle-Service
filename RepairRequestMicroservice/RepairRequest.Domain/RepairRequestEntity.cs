using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairRequest.Domain
{
    public class RepairRequestEntity
    {
        public int Id { get; init; }
        public int UserId { get; set; }
        public virtual AppUser User { get; set; }
        public int? MasterId { get; set; }
        public virtual AppUser Master { get; set; }
        public int StatusId { get; set; }
        public virtual RequestStatus RequestStatus { get; set; }
        public int ServiceCenterId { get; set; }
        public DateTime LeadTime { get; set; }
    }
}
