using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.dto
{
    public class ServiceCenterDto
    {
        public int Id { get; init; }
        public string Name { get; set; }
        
        public string Location { get; set; }
    }
}
