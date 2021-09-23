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

        public void RegisterShop(Shop newShop)
        {
            if (newShop == null)
            {
                throw new ShopException("Shop is null");
            }

            if (_shops.Contains(newShop))
            {
                throw new ShopException("This shop is already added");
            }

            _shops.Add(newShop);
        }

        public void RegisterProduct(Product newProduct)
        {
            if (newProduct == null)
            {
                throw new ShopException("Product is null");
            }

            if (_products.Contains(newProduct))
            {
                throw new ShopException("This product is already added");
            }

            _products.Add(newProduct);
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