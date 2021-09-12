using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using Shops.Tools;

namespace Shops.Entities
{
    public class Shop
    {
        private Dictionary<Product, ShopItem> _products;

        public Shop(string name)
        {
            _products = new Dictionary<Product, ShopItem>();
            Id = Guid.NewGuid();
            Name = name;
        }

        public Guid Id { get; }

        public string Name { get; set; }

        public void RegisterProduct(Product product, uint price, uint count = 0)
        {
            _products.Add(product, new ShopItem(price, count));
        }

        public void AddProducts(Product product, uint count)
        {
            if (!_products.ContainsKey(product))
            {
                throw new ShopException();
            }

            _products[product].Count += count;
        }

        public void ReserveProducts(Product product, uint count)
        {
            if (!_products.ContainsKey(product))
            {
                throw new ShopException();
            }

            if (_products[product].Count < count)
            {
                throw new ShopException();
            }

            _products[product].Count -= count;
        }

        public void ChangePrice(Product product, uint newPrice)
        {
            if (!_products.ContainsKey(product))
            {
                throw new ShopException();
            }

            _products[product].Price = newPrice;
        }

        public void Buy(Person person, IReadOnlyDictionary<Product, uint> shoppingList)
        {
            foreach (KeyValuePair<Product, uint> item in shoppingList)
            {
                if (!_products.ContainsKey(item.Key))
                {
                    throw new ShopException();
                }

                if (_products[item.Key].Count < item.Value)
                {
                    throw new ShopException();
                }

                if (_products[item.Key].Price * item.Value > person.Balance)
                {
                    throw new ShopException();
                }

                _products[item.Key].Count -= item.Value;
                person.Balance -= _products[item.Key].Price * item.Value;
            }
        }

        public ShopItem GetProductInfo(Product product)
        {
            if (!_products.ContainsKey(product))
            {
                throw new ShopException();
            }

            return _products[product];
        }

        public bool ContainsProduct(Product product)
        {
            return _products.ContainsKey(product);
        }
    }
}