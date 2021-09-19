using System;
using System.Collections.Generic;
using System.Linq;
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
        public string Name { get; }

        public void RegisterProduct(Product product, uint price, uint count = 0)
        {
            if (product == null)
            {
                throw new ShopException("your product should be is not null");
            }

            _products.Add(product, new ShopItem(price, count));
        }

        public void AddProducts(Product product, uint count)
        {
            if (!_products.ContainsKey(product))
            {
                throw new ShopException("This shop doesn't contain this product");
            }

            _products[product].Count += count;
        }

        public void ReserveProducts(Product product, uint count)
        {
            if (!_products.ContainsKey(product))
            {
                throw new ShopException("This shop doesn't contain this product");
            }

            if (_products[product].Count < count)
            {
                throw new ShopException("This shop doesn't contain required quantity of this product");
            }

            _products[product].Count -= count;
        }

        public void ChangePrice(Product product, uint newPrice)
        {
            if (!_products.ContainsKey(product))
            {
                throw new ShopException("This shop doesn't contain this product");
            }

            _products[product].Price = newPrice;
        }

        public void Buy(Person person, IReadOnlyDictionary<Product, Count> shoppingList)
        {
            foreach ((Product product, Count count) in shoppingList)
            {
                if (!_products.ContainsKey(product))
                {
                    throw new ShopException("This shop doesn't contain this product");
                }

                if (_products[product].Count < count.Value)
                {
                    throw new ShopException("This shop doesn't contain required quantity of this product");
                }

                if (_products[product].Price * count.Value > person.Balance)
                {
                    throw new ShopException("This person doesn't have required amount of money");
                }

                _products[product].Count -= count.Value;
                person.Balance -= _products[product].Price * count.Value;
            }
        }

        public void Buy(Person person, Product product, uint count)
        {
            var shoppingList = new Dictionary<Product, Count>();
            shoppingList.Add(product, new Count(count));
            Buy(person, shoppingList);
        }

        public ShopItem GetProductInfo(Product product)
        {
            if (!_products.ContainsKey(product))
            {
                throw new ShopException("This shop doesn't contain this product");
            }

            return _products[product];
        }

        public bool ContainsProduct(Product product)
        {
            return _products.ContainsKey(product);
        }

        public bool ContainsProducts(IReadOnlyDictionary<Product, Count> shoppingList)
        {
            if (!shoppingList.All(item => ContainsProduct(item.Key)))
            {
                return false;
            }

            if (!shoppingList.All(item => GetProductInfo(item.Key).Count >= item.Value.Value))
            {
                return false;
            }

            return true;
        }

        public IReadOnlyList<Product> GetListOfProducts()
        {
            return _products.Select(item => item.Key).ToList();
        }

        public uint CalculatePriceOfShoppingList(IReadOnlyDictionary<Product, Count> shoppingList)
        {
            if (!ContainsProducts(shoppingList))
            {
                throw new ShopException("This shop doesnt contains required amount of products");
            }

            return shoppingList.Aggregate<KeyValuePair<Product, Count>, uint>(
                0,
                (current, item) => current + (GetProductInfo(item.Key).Price * item.Value.Value));
        }
    }
}