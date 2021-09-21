using System.Collections.Generic;
using System.Linq;
using Shops.Entities;
using Shops.UI.Models;
using Spectre.Console;

namespace Shops.UI.Menu
{
    public class ProductsMenu : Menu
    {
        private Shop _shop;
        private IReadOnlyList<Product> _products;

        public ProductsMenu(Shop shop, List<ShoppingListItem> shoppingList, ClientContext context, Menu prevMenu)
            : base(prevMenu, shoppingList, context)
        {
            _shop = shop;
        }

        public override IMenu GenerateNextMenu()
        {
            if (Choice == "Back")
            {
                return PrevMenu;
            }

            Product selectedProduct = _products[SelectionOptions.IndexOf(Choice)];
            Context.CurrentShop = _shop;
            Context.CurrentProduct = selectedProduct;
            return new ItemMenu(
                ShoppingList,
                Context,
                this);
        }

        public override void UpdateTable()
        {
            SelectionOptions = new List<string>();
            Table = new Table();
            Table.AddColumns("N", "Name", "Price", "Count");
            _products = _shop.GetListOfProducts();
            uint number = 1;
            foreach (Product product in _products)
            {
                Table.AddRow(
                    number.ToString(),
                    product.Name,
                    _shop.GetProductInfo(product).Price.ToString(),
                    _shop.GetProductInfo(product).Count.ToString());
                SelectionOptions.Add(product.Name);
                ++number;
            }

            SelectionOptions.Add("Back");
        }
    }
}