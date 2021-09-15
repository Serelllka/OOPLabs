using System.Collections.Generic;
using System.Data;
using Shops.Entities;
using Spectre.Console;

namespace Shops.UI.Menu
{
    public class ProductsMenu : IMenu
    {
        private List<string> _selectionOptions;
        private List<ShoppingListItem> _shoppingList;
        private Table _table;
        private IMenu _prevMenu;
        private Shop _shop;
        private IReadOnlyList<Product> _products;

        public ProductsMenu(Shop shop, List<ShoppingListItem> shoppingList, IMenu prevMenu)
        {
            _shoppingList = shoppingList;
            _prevMenu = prevMenu;
            _shop = shop;
            UpdateTable();
        }

        public string Choice { get; private set; }

        public void Show()
        {
            UpdateTable();
            AnsiConsole.Render(_table);
            Choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .PageSize(10)
                    .AddChoices(_selectionOptions));
        }

        public IMenu GenerateNextMenu()
        {
            if (Choice == _selectionOptions[^1])
            {
                return _prevMenu;
            }

            Product selectedProduct = _products[_selectionOptions.IndexOf(Choice)];
            return new ItemMenu(
                selectedProduct,
                _shop.GetProductInfo(selectedProduct),
                _shoppingList,
                this);
        }

        public void UpdateTable()
        {
            _selectionOptions = new List<string>();
            _table = new Table();
            _table.AddColumns("N", "Name", "Price", "Count");
            _products = _shop.GetListOfProducts();
            uint number = 1;
            foreach (Product product in _products)
            {
                _table.AddRow(
                    number.ToString(),
                    product.Name,
                    _shop.GetProductInfo(product).Price.ToString(),
                    _shop.GetProductInfo(product).Count.ToString());
                _selectionOptions.Add(product.Name);
                ++number;
            }

            _selectionOptions.Add("Back");
        }
    }
}