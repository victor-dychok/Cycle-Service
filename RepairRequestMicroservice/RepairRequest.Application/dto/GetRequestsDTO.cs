using RepairRequest.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairRequest.Application.dto
{
    public class GetRequestsDTO
    {
        public int Id { get; init; }
        public int UserId { get; init; }
        public int MasterId { get; init; }
        public virtual IEnumerable<RepairRequestComponent> Components { get; set; }
        public virtual IEnumerable<RepairRequestService> Services { get; set; }
        public int StatusId { get; set; }
        public int ServiceCenterId { get; set; }

    }
}
