using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain
{
    public class Component
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
        public int Price { get; private set; }
        public void UpdatePrice(int price)
        {
            if (price < 0 || price > int.MaxValue)
            {
                throw new ArgumentException("Invalid price", nameof(price));
            }
            else
            {
                Price = price;
            }
        }
    }
}
