using System;
using Shops.Tools;

namespace Shops.Entities
{
    public class Product
    {
        public Product(string name)
        {
            Name = name;
            Id = Guid.NewGuid();
        }

        public string Name { get; }
        public Guid Id { get; }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Product prod))
            {
                return false;
            }

            return Id == prod.Id;
        }
    }
}