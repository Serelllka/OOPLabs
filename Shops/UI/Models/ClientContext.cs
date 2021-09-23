#nullable enable
using System.Collections.Generic;
using Shops.Entities;
using Shops.Services;
using Shops.Tools;

namespace Shops.UI.Models
{
    public class ClientContext
    {
        private readonly ShopManager _shopManager;
        public ClientContext(ShopManager shopManager, Person customer)
        {
            Customer = customer;
            _shopManager = shopManager;
        }

        public IReadOnlyList<Shop> Shops => _shopManager.Shops;
        public Person Customer { get; }
        public Shop? CurrentShop { get; set; }
        public Product? CurrentProduct { get; set; }
        public ShopItem CurrentShopItem
        {
            get
            {
                if (CurrentShop is null)
                {
                    throw new ShopException("CurrentShop can't be null");
                }

                if (CurrentProduct is null)
                {
                    throw new ShopException("CurrentProduct can't be null");
                }

                return CurrentShop.GetProductInfo(CurrentProduct);
            }
        }

        public IReadOnlyList<Product> GetCurrentProducts()
        {
            if (CurrentShop == null)
            {
                throw new ShopException("CurrentShop can't be null");
            }

            return CurrentShop.GetListOfProducts();
        }
    }
}