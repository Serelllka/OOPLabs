using System.Collections.Generic;
using Shops.Entities;
using Shops.Models;
using Shops.Tools;
using Spectre.Console;

namespace Shops.UI.Menu
{
    public class CartMenu : Menu
    {
        private Person _person;
        public CartMenu(Person person, List<ShoppingListItem> shoppingList, IMenu prevMenu)
            : base(prevMenu, shoppingList)
        {
            _person = person;
        }

        public override IMenu GenerateNextMenu()
        {
            if (Choice == "Buy")
            {
                foreach (ShoppingListItem item in ShoppingList)
                {
                    item.BuyThisItem(_person);
                }

                return this;
            }

            if (Choice == "-")
            {
                return this;
            }

            if (Choice == "Back")
            {
                return PrevMenu;
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
            foreach (ShoppingListItem item in ShoppingList)
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