using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Shops.Entities;
using Shops.Services;
using Shops.Tools;
using Shops.UI;

namespace Shops.Tests
{
    public class Tests
    {
        private ShopManager _shopManager;

        [SetUp]
        public void Setup()
        {
            _shopManager = new ShopManager();
        }

        [Test]
        public void AddShopAndProduct()
        {
            const int moneyBefore = 300;
            const int productPrice = 30;
            const int productCount = 5;
            const int productToBuyCount = 3;

            var person = new Person("Billy Herrington", moneyBefore);
            Shop shop = _shopManager.Create("VisitingDungeonMaster");
            Product product = _shopManager.RegisterProduct("Fisting");

            var shoppingList = new Dictionary<Product, uint>();
            shoppingList.Add(product, productToBuyCount);
            
            shop.RegisterProduct(product, productPrice, productCount);
            shop.Buy(person, shoppingList);

            Assert.AreEqual(moneyBefore - productPrice * productToBuyCount, person.Balance);
            Assert.AreEqual(productCount - productToBuyCount, shop.GetProductInfo(product).Count);
        }

        [Test]
        public void SutUpNewPrice()
        {
            const int priceBefore = 300;
            const int priceAfter = 228;

            Shop shop = _shopManager.Create("Dungeon");
            Product product = _shopManager.RegisterProduct("Cocaine");
            
            shop.RegisterProduct(product, priceBefore, 5);
            shop.ChangePrice(product, priceAfter);
            
            Assert.AreEqual(shop.GetProductInfo(product).Price, priceAfter);
        }

        [Test]
        public void FindTheLowestPrice()
        {
            Shop shop1 = _shopManager.Create("Shop1");
            Shop shop2 = _shopManager.Create("Shop2");
            Shop shop3 = _shopManager.Create("Shop3");
            Shop shop4 = _shopManager.Create("Shop4");

            Product cheese = _shopManager.RegisterProduct("Russian's Cheese");
            Product lard = _shopManager.RegisterProduct("Ukrainian's Lard");
            
            var shoppingList = new Dictionary<Product, uint>();
            shoppingList.Add(cheese, 2);
            shoppingList.Add(lard, 4);
            
            shop1.RegisterProduct(cheese, 10, 2);
            shop1.RegisterProduct(lard, 1, 3);
            
            shop2.RegisterProduct(cheese, 1, 100);
            
            shop3.RegisterProduct(cheese, 10, 2);
            shop3.RegisterProduct(lard, 100, 4);
            
            shop4.RegisterProduct(cheese, 1, 2000);
            shop4.RegisterProduct(lard, 1000, 1000);
            
            Assert.AreEqual(_shopManager.FindOptimalShop(shoppingList), shop3);
        }
        
        [Test]
        public void BuyUsingShoppingList()
        {
            const int moneyBefore = 300;
            
            const int cheesePrice = 30;
            const int cheeseCount = 5;
            const int cheeseToBuyCount = 3;
            
            const int lardPrice = 40;
            const int lardCount = 6;
            const int lardToBuyCount = 4;

            var person = new Person("Billy Herrington", moneyBefore);
            Shop shop = _shopManager.Create("VisitingDungeonMaster");
            
            Product cheese = _shopManager.RegisterProduct("Russian's Cheese");
            Product lard = _shopManager.RegisterProduct("Ukrainian's Lard");
            
            var shoppingList = new Dictionary<Product, uint>();
            shoppingList.Add(cheese, cheeseToBuyCount);
            shoppingList.Add(lard, lardToBuyCount);
            
            shop.RegisterProduct(cheese, cheesePrice, cheeseCount);
            shop.RegisterProduct(lard, lardPrice, lardCount);
            
            shop.Buy(person, shoppingList);

            Assert.AreEqual(moneyBefore - lardPrice * lardToBuyCount - cheesePrice * cheeseToBuyCount, person.Balance);
            Assert.AreEqual(cheeseCount - cheeseToBuyCount, shop.GetProductInfo(cheese).Count);
            Assert.AreEqual(lardCount - lardToBuyCount, shop.GetProductInfo(lard).Count);
        }
    }
}