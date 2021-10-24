using System.Collections.Generic;
using Shops.Entities;
using Shops.Services;
using Shops.Tools;
using Shops.UI;
using Shops.UI.Menu;
using Spectre.Console;

namespace Shops
{
    internal class Program
    {
        private static void Main()
        {
            var shopManager = new ShopManager();

            const int priceBefore = 300;
            const int priceAfter = 228;

            var shop = new Shop("Dungeon");
            shopManager.RegisterShop(shop);
            var cocaine = new Product("Cocaine");
            var cheese = new Product("Russian's Cheese");
            var lard = new Product("Lard");
            shopManager.RegisterProduct(cocaine);
            shopManager.RegisterProduct(cheese);
            shopManager.RegisterProduct(lard);

            shop.RegisterProduct(cocaine, priceBefore, 5);
            shop.RegisterProduct(cheese, priceBefore, 4);
            shop.RegisterProduct(lard, priceBefore, 20);
            shop.ChangePrice(cocaine, priceAfter);

            var person = new Person("Igor", 300);
            var menuManager = new MenuManager(shopManager, person);

            menuManager.Start();
        }
    }
}