using System.Collections.Generic;
using System.Linq;
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

        public Shop FindOptimalShop(IReadOnlyDictionary<Product, uint> shoppingList)
        {
            var suitableShops = new List<Shop>(_shops);
            suitableShops = shoppingList.Aggregate(suitableShops, (current, item) =>
                current.FindAll(shop => shop.ContainsProduct(item.Key) && shop.GetProductInfo(item.Key).Count >= item.Value));

            uint minPurchaseAmount = uint.MaxValue;
            Shop obtainedShop = suitableShops[0];
            foreach (Shop shop in suitableShops)
            {
                uint purchaseAmount = shoppingList.Aggregate<KeyValuePair<Product, uint>, uint>(
                    0, (current, item) => current + (item.Value * shop.GetProductInfo(item.Key).Price));

                if (purchaseAmount >= minPurchaseAmount) continue;
                minPurchaseAmount = purchaseAmount;
                obtainedShop = shop;
            }

            return obtainedShop;
        }
    }
}