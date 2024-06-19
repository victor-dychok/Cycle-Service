using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain
{
    public class ServiceCenterEntity
    {
        public int Id { get; init; }
        public string Name { get; private set; }
        public void UpdateLogin(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name is empty", nameof(name));
            }
            else if (name.Length > 50 || name.Length < 2)
            {
                throw new ArgumentException("Name length must be from 2 to 50 symbols", nameof(name));
            }
            else
            {
                Name = name;
            }
        }
        public string Location { get; private set; }
        public void UpdateLocation(string location)
        {
            if (string.IsNullOrEmpty(location))
            {
                throw new ArgumentException("Location is empty", nameof(location));
            }
            else
            {
                Location = location;
            }
        }
        public int AdminId { get; set; }
        public virtual AppUser Admin { get; set; }
        public virtual IEnumerable<ServiceCenterUser> Stuf { get; set; }
    }
}
