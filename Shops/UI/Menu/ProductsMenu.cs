using System.Collections.Generic;
using System.Data;
using Shops.Entities;
using Spectre.Console;

namespace Shops.UI.Menu
{
    public class ProductsMenu : Menu, IMenu
    {
        private List<ShoppingListItem> _shoppingList;
        private Shop _shop;
        private IReadOnlyList<Product> _products;

        public ProductsMenu(Shop shop, List<ShoppingListItem> shoppingList, IMenu prevMenu)
            : base(prevMenu)
        {
            _shoppingList = shoppingList;
            _shop = shop;
        }

        public IMenu GenerateNextMenu()
        {
            if (Choice == SelectionOptions[^1])
            {
                return PrevMenu;
            }

            Product selectedProduct = _products[SelectionOptions.IndexOf(Choice)];
            return new ItemMenu(
                _shop,
                selectedProduct,
                _shop.GetProductInfo(selectedProduct),
                _shoppingList,
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