using System.Collections.Generic;
using Shops.Entities;
using Spectre.Console;

namespace Shops.UI.Menu
{
    public class ProductsMenu : Menu
    {
        private Shop _shop;
        private IReadOnlyList<Product> _products;

        public ProductsMenu(Shop shop, List<ShoppingListItem> shoppingList, IMenu prevMenu)
            : base(prevMenu, shoppingList)
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
            return new ItemMenu(
                _shop,
                selectedProduct,
                _shop.GetProductInfo(selectedProduct),
                ShoppingList,
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