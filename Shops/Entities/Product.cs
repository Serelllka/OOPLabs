using System;
using Shops.Tools;

namespace Shops.Entities
{
    public class Product
    {
        public Product(string name)
        {
            Name = name ?? throw new ShopException("Can't create product with null name");
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
            return obj is Product prod && Id == prod.Id;
        }
    }
}