using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Shops.Entities;
using Shops.Services;
using Shops.Tools;
using Shops.ValueObject;

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
        public void AddShopAndProductPersonBuyProduct_PersonMoneyDecreases()
        {
            const int moneyBefore = 300;
            const int productPrice = 30;
            const int productCount = 5;
            const int productToBuyCount = 3;

            var person = new Person("Billy Herrington", moneyBefore);
            var shop = new Shop("VisitingDungeonMaster");
            _shopManager.RegisterShop(shop);
            var product = new Product("Fisting");
            _shopManager.RegisterProduct(product);

            var shoppingList = new Dictionary<Product, Count>();
            shoppingList.Add(product, new Count(productToBuyCount));
            
            shop.RegisterProduct(product, productPrice, productCount);
            shop.Buy(person, shoppingList);

            Assert.AreEqual(moneyBefore - productPrice * productToBuyCount, person.Balance);
            Assert.AreEqual(productCount - productToBuyCount, shop.GetProductInfo(product).Count);
        }

        [Test]
        public void SutUpNewPrice_ProductPriceChanges()
        {
            const int priceBefore = 300;
            const int priceAfter = 228;

            var shop = new Shop("Dungeon");
            _shopManager.RegisterShop(shop);
            var product = new Product("Cocaine");
            _shopManager.RegisterProduct(product);
            
            shop.RegisterProduct(product, priceBefore, 5);
            shop.ChangePrice(product, priceAfter);
            
            Assert.AreEqual(shop.GetProductInfo(product).Price, priceAfter);
        }

        [Test] public void FindTheLowestPrice_ReturnShopWithMinimumTotalPrice()
        {
            var shop1 = new Shop("Shop1");
            var shop2 = new Shop("Shop2");
            var shop3 = new Shop("Shop3");
            var shop4 = new Shop("Shop4");
            
            _shopManager.RegisterShop(shop1);
            _shopManager.RegisterShop(shop2);
            _shopManager.RegisterShop(shop3);
            _shopManager.RegisterShop(shop4);

            var cheese = new Product("Russian's Cheese");
            var lard = new Product("Ukrainian's Lard");
            _shopManager.RegisterProduct(cheese);
            _shopManager.RegisterProduct(lard);
            
            var shoppingList = new Dictionary<Product, Count>();
            shoppingList.Add(cheese, new Count(2));
            shoppingList.Add(lard, new Count(4));
            
            shop1.RegisterProduct(cheese, 10, 2);
            shop1.RegisterProduct(lard, 1, 3);
            
            shop2.RegisterProduct(cheese, 1, 100);
            
            shop3.RegisterProduct(cheese, 10, 2);
            shop3.RegisterProduct(lard, 100, 4);
            
            shop4.RegisterProduct(cheese, 1, 2000);
            shop4.RegisterProduct(lard, 1000, 1000);
            
            Assert.AreEqual(_shopManager.FindOptimalShopByPrice(shoppingList), shop3);
        }
        
        [Test]
        public void BuyUsingShoppingList_ReduceProductsAmountInShop()
        {
            const int moneyBefore = 300;
            
            const int cheesePrice = 30;
            const int cheeseCount = 5;
            const int cheeseToBuyCount = 3;
            
            const int lardPrice = 40;
            const int lardCount = 6;
            const int lardToBuyCount = 4;

            var person = new Person("Billy Herrington", moneyBefore);
            var shop = new Shop("VisitingDungeonMaster");
            _shopManager.RegisterShop(shop);
            
            var cheese = new Product("Russian's Cheese");
            var lard = new Product("Ukrainian's Lard");
            _shopManager.RegisterProduct(cheese);
            _shopManager.RegisterProduct(lard);
            
            var shoppingList = new Dictionary<Product, Count>();
            shoppingList.Add(cheese, new Count(cheeseToBuyCount));
            shoppingList.Add(lard, new Count(lardToBuyCount));
            
            shop.RegisterProduct(cheese, cheesePrice, cheeseCount);
            shop.RegisterProduct(lard, lardPrice, lardCount);
            
            shop.Buy(person, shoppingList);

            Assert.AreEqual(moneyBefore - lardPrice * lardToBuyCount - cheesePrice * cheeseToBuyCount, person.Balance);
            Assert.AreEqual(cheeseCount - cheeseToBuyCount, shop.GetProductInfo(cheese).Count);
            Assert.AreEqual(lardCount - lardToBuyCount, shop.GetProductInfo(lard).Count);
        }

        [Test]
        public void GiveNullParameters_ThrowsException()
        {
            Assert.Catch<ShopException>(() =>
            {
                var person = new Person(null, 1000);
            });

            Assert.Catch<ShopException>(() =>
            {
                var shop = new Shop(null);
            });

            Assert.Catch<ShopException>(() =>
            {
                var product = new Product(null);
            });
        }
        [Test]
        public void TryToBuyShoppingList_ThrowsExceptionMoneyAndProductAmountIsNotChanged()
        {
            var shop = new Shop("Shop");
            _shopManager.RegisterShop(shop);

            var cheese = new Product("Russian's Cheese");
            var lard = new Product("Ukrainian's Lard");
            
            var shoppingList = new Dictionary<Product, Count>();
            shoppingList.Add(cheese, new Count(1));
            shoppingList.Add(lard, new Count(1));
            
            shop.RegisterProduct(cheese, 10, 2);
            shop.RegisterProduct(lard, 1000, 3);

            var person = new Person("Freddi", 10);
            
            Assert.Catch<ShopException>(() =>
            {
               shop.Buy(person, shoppingList);
            });
            
            Assert.AreEqual(person.Balance, 10);
            Assert.AreEqual(shop.GetProductInfo(cheese).Count, 2);
        }
        
    }
}