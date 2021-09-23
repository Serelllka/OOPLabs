using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Tools;
using Shops.ValueObject;

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
                throw new ShopException("This product doesn't exist");
            }

            _products.Add(product, new ShopItem(price, count, product));
        }

        public void AddProducts(Product product, uint count)
        {
            GetProductInfo(product).Count += count;
        }

        public void ReserveProducts(Product product, uint count)
        {
            ShopItem obtainedProduct = GetProductInfo(product);

            if (obtainedProduct.Count < count)
            {
                throw new ShopException("This shop doesn't contain required quantity of this product");
            }

            obtainedProduct.Count -= count;
        }

        public void ChangePrice(Product product, uint newPrice)
        {
            ShopItem obtainedProduct = GetProductInfo(product);

            obtainedProduct.Price = newPrice;
        }

        public uint CalculateTotalPrice(IReadOnlyDictionary<Product, Count> shoppingList)
        {
            uint totalPrice = 0;
            foreach ((Product product, Count count) in shoppingList)
            {
                totalPrice += GetProductInfo(product).Price * count.Value;
            }

            return totalPrice;
        }

        public void Buy(Person person, IReadOnlyDictionary<Product, Count> shoppingList)
        {
            if (CalculateTotalPrice(shoppingList) > person.Balance)
            {
                throw new ShopException("This person doesn't have required amount of money");
            }

            if (!ContainsProducts(shoppingList))
            {
                throw new ShopException("This shop doesn't contain required quantity of this product");
            }

            foreach ((Product product, Count count) in shoppingList)
            {
                ShopItem obtainedProduct = GetProductInfo(product);
                obtainedProduct.Count -= count.Value;
                person.ReduceMoney(obtainedProduct.Price * count.Value);
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
            if (!_products.TryGetValue(product, out ShopItem shopItem))
            {
                throw new ShopException("This shop doesn't contain this product");
            }

            return shopItem;
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

            int totalPrice = shoppingList.Aggregate(
                0,
                (current, item) => (int)(current + (GetProductInfo(item.Key).Price * item.Value.Value)));
            return (uint)totalPrice;
        }
    }
}