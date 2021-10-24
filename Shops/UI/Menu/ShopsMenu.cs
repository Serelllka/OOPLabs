using System.Collections.Generic;
using Shops.Entities;
using Shops.UI.Models;
using Spectre.Console;

namespace Shops.UI.Menu
{
    public class ShopsMenu : Menu
    {
        private IReadOnlyList<Shop> _shops;

        public ShopsMenu(
            IReadOnlyList<Shop> shops,
            List<ShoppingListItem> shoppingList,
            ClientContext context,
            Menu prevMenu)
            : base(prevMenu, shoppingList, context)
        {
            _shops = shops;
        }

        public override void UpdateTable()
        {
            SelectionOptions = new List<string>();
            Table = new Table();
            Table.AddColumns("N", "Name", "Different products");

            uint number = 1;
            foreach (Shop shop in _shops)
            {
                Table.AddRow(
                    number.ToString(),
                    shop.Name,
                    shop.GetListOfProducts().Count.ToString());
                SelectionOptions.Add(shop.Name);
                ++number;
            }

            SelectionOptions.Add("Back");
        }
    }
}