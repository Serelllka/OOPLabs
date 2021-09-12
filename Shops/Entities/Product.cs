using System;
using Shops.Tools;

namespace Shops.Entities
{
    public class Product
    {
        public Product(string name)
        {
            Name = name;
            Id = new Guid();
        }

        public string Name { get; }
        public Guid Id { get; }
    }
}