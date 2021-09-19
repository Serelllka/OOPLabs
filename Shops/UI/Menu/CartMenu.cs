using System.Collections.Generic;
using System.Data;
using Shops.Entities;
using Shops.Services;
using Shops.Tools;
using Spectre.Console;

namespace Shops.UI.Menu
{
    public class CartMenu : Menu, IMenu
    {
        private Person _person;
        private List<ShoppingListItem> _shoppingList;
        public CartMenu(Person person, List<ShoppingListItem> shoppingList, IMenu prevMenu)
            : base(prevMenu)
        {
            _person = person;
            _shoppingList = shoppingList;
        }

        public IMenu GenerateNextMenu()
        {
            if (Choice == SelectionOptions[^1])
            {
                return PrevMenu;
            }

            if (Choice == "Buy")
            {
                foreach (ShoppingListItem item in _shoppingList)
                {
                    item.BuyThisItem(_person);
                }

                return this;
            }

            if (Choice == "-")
            {
                return this;
            }

            throw new ShopException("CartMenu can't handle this choice");
        }

        public override void UpdateTable()
        {
            Table = new Table();
            SelectionOptions = new List<string>();
            Table.AddColumns("N", "Title", "Amount", "Price", "Total");
            uint number = 1;
            uint totalPrice = 0;
            foreach (ShoppingListItem item in _shoppingList)
            {
                Table.AddRow(
                    number.ToString(),
                    item.Name,
                    item.Count.ToString(),
                    item.Price.ToString(),
                    (item.Count * item.Price).ToString());
                totalPrice += item.Count * item.Price;
                number += 1;
            }

            Table.AddRow("-", "-", "-", "-", "-");
            Table.AddRow("Person:", _person.Name, "Balance:", _person.Balance.ToString());
            if (_person.Balance < totalPrice)
            {
                Table.AddRow("Not enough money ", "Total price:", totalPrice.ToString(), "-", "-");
                SelectionOptions.Add("-"); // this field is added because one field is not displayed correctly
            }
            else
            {
                SelectionOptions.Add("Buy");
            }

            SelectionOptions.Add("Back");
        }
    }
}