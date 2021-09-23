using System.Collections.Generic;
using Shops.Entities;
using Shops.Services;
using Shops.Tools;
using Shops.UI.Models;

namespace Shops.UI.Menu
{
    public class MainMenu : Menu
    {
        public MainMenu(
            List<ShoppingListItem> shoppingList,
            ClientContext context,
            Menu prevMenu = null)
            : base(prevMenu, shoppingList, context)
        {
        }

        public override void UpdateTable()
        {
            SelectionOptions = new List<string>();

            if (Context.Shops.Count > 0)
            {
                SelectionOptions.Add("List of shops");
            }

            if (ShoppingList.Count > 0)
            {
                SelectionOptions.Add("Shopping List");
            }

            if (PrevMenu != null)
            {
                SelectionOptions.Add("Back");
            }

            SelectionOptions.Add("Exit");

            if (SelectionOptions.Count == 1)
            {
                SelectionOptions.Add("-");  // this field is added because one field is not displayed correctly
            }
        }
    }
}