using System.Collections.Generic;
using Shops.Entities;

namespace Shops.Services
{
    public class ShopManager
    {
        private List<Product> _products;
        private List<Shop> _shops;

        public ShopManager()
        {
            _products = new List<Product>();
            _shops = new List<Shop>();
        }

        public Shop Create(string name)
        {
            var shop = new Shop(name);
            _shops.Add(shop);
            return shop;
        }

        public Product RegisterProduct(string name)
        {
            var product = new Product(name);
            _products.Add(product);
            return product;
        }
    }
}