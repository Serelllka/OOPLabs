using System.Collections.Generic;
using System.Linq;
using Shops.Entities;
using Shops.Tools;
using Shops.ValueObject;

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

        public IReadOnlyList<Shop> Shops => _shops;
        public IReadOnlyList<Product> Products => _products;

        public Shop CreateShop(string name)
        {
            if (name == null)
            {
                throw new ShopException("Can't create shop with null name");
            }

            var shop = new Shop(name);
            _shops.Add(shop);
            return shop;
        }

        public Product RegisterProduct(string name)
        {
            if (name == null)
            {
                throw new ShopException("Can't create shop with null name");
            }

            var product = new Product(name);
            _products.Add(product);
            return product;
        }

        public Shop FindOptimalShopByPrice(IReadOnlyDictionary<Product, Count> shoppingList)
        {
            uint minPurchaseAmount = uint.MaxValue;
            Shop obtainedShop = null;
            foreach (Shop shop in _shops)
            {
                if (!shop.ContainsProducts(shoppingList))
                {
                    continue;
                }

                uint purchaseAmount = shop.CalculatePriceOfShoppingList(shoppingList);

                if (purchaseAmount < minPurchaseAmount)
                {
                    minPurchaseAmount = purchaseAmount;
                    obtainedShop = shop;
                }
            }

            return obtainedShop;
        }
    }
}