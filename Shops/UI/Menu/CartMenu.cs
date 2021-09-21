using System.Collections.Generic;
using Shops.Entities;
using Shops.Tools;
using Shops.UI.Models;
using Spectre.Console;

namespace Shops.UI.Menu
{
    public class CartMenu : Menu
    {
        public CartMenu(List<ShoppingListItem> shoppingList, ClientContext context, Menu prevMenu)
            : base(prevMenu, shoppingList, context)
        {
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
            Table.AddRow("Person:", Context.Customer.Name, "Balance:", Context.Customer.Balance.ToString());
            if (Context.Customer.Balance < totalPrice)
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