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
            ShopManager shopManager = new ShopManager();

            const int priceBefore = 300;
            const int priceAfter = 228;

            Shop shop = shopManager.CreateShop("Dungeon");
            shopManager.CreateShop("NeDungeon");
            Product cocaine = shopManager.RegisterProduct("Cocaine");
            Product cheese = shopManager.RegisterProduct("Russian's Cheese");
            Product lard = shopManager.RegisterProduct("Lard");

            shop.RegisterProduct(cocaine, priceBefore, 5);
            shop.RegisterProduct(cheese, priceBefore, 4);
            shop.RegisterProduct(lard, priceBefore, 20);
            shop.ChangePrice(cocaine, priceAfter);

            Person person = new Person("Igor", 300);

            MenuManager menuManager = new MenuManager(shopManager, person);
            menuManager.Start();
        }
    }
}