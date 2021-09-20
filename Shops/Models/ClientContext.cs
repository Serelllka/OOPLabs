using System.Collections.Generic;
using Shops.Entities;

namespace Shops.Models
{
    public class ClientContext
    {
        public ClientContext(List<ShoppingListItem> shoppingList)
        {
            ShoppingList = shoppingList;
        }

        public List<ShoppingListItem> ShoppingList { get; private set; }
    }
}