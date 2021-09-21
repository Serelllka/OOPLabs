using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Entities;
using Shops.UI.Menu;

namespace Shops.Tools
{
    public class MenuFactory
    {
        private Menu _currentMenu;
        public MenuFactory(Menu currentMenu)
        {
            _currentMenu = currentMenu;
        }

        public Menu CurrentMenu => _currentMenu;

        public Menu CreateMenu(string selectedOption)
        {
            switch (selectedOption)
            {
                case "-":
                    return _currentMenu;

                case "Back":
                    _currentMenu = _currentMenu.PrevMenu;
                    return _currentMenu;

                case "Buy":
                    foreach (ShoppingListItem item in _currentMenu.ShoppingList)
                    {
                        item.BuyThisItem(_currentMenu.Context.Customer);
                    }

                    return _currentMenu;

                case "Add to card":
                    if (_currentMenu.ShoppingList.All(item =>
                        !Equals(item.Item, _currentMenu.Context.CurrentProduct)))
                    {
                        _currentMenu.ShoppingList.Add(new ShoppingListItem(
                            _currentMenu.Context.CurrentShop,
                            _currentMenu.Context.CurrentProduct,
                            _currentMenu.Context.CurrentShopItem.Price));
                    }

                    _currentMenu.ShoppingList.First(item =>
                        Equals(item.Item, _currentMenu.Context.CurrentProduct)).Count += 1;
                    return _currentMenu;

                case "Remove from card":
                    if (_currentMenu.Context.CurrentProduct is null)
                    {
                        throw new ShopException("CurrentProduct can't be null");
                    }

                    _currentMenu.ShoppingList.First(item =>
                        item.Item.Id == _currentMenu.Context.CurrentProduct.Id).Count -= 1;
                    return _currentMenu;

                case "List of shops":
                    _currentMenu = new ShopsMenu(
                        _currentMenu.Context.Shops,
                        _currentMenu.ShoppingList,
                        _currentMenu.Context,
                        _currentMenu);
                    return _currentMenu;

                case "Shopping List":
                    _currentMenu = new CartMenu(
                        _currentMenu.ShoppingList,
                        _currentMenu.Context,
                        _currentMenu);
                    return _currentMenu;
            }

            if (_currentMenu is ProductsMenu)
            {
                _currentMenu.Context.CurrentProduct = _currentMenu.Context.GetCurrentProducts()[_currentMenu.SelectionOptions.IndexOf(selectedOption)];
                _currentMenu = new ItemMenu(
                    _currentMenu.ShoppingList,
                    _currentMenu.Context,
                    _currentMenu);
                return _currentMenu;
            }

            if (_currentMenu is ShopsMenu)
            {
                _currentMenu.Context.CurrentShop =
                    _currentMenu.Context.Shops[_currentMenu.SelectionOptions.IndexOf(selectedOption)];
                _currentMenu = new ProductsMenu(
                    _currentMenu.Context.CurrentShop,
                    _currentMenu.ShoppingList,
                    _currentMenu.Context,
                    _currentMenu);
                return _currentMenu;
            }

            throw new ShopException("Can't handle this choice");
        }
    }
}