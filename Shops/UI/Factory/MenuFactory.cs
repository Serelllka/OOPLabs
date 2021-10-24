using System.Linq;
using Shops.Entities;
using Shops.Tools;
using Shops.UI.Commands;
using Shops.UI.Menu;

namespace Shops.UI.Factory
{
    public class MenuFactory
    {
        private Command _command;
        private Menu.Menu _currentMenu;
        public MenuFactory(Menu.Menu currentMenu)
        {
            _currentMenu = currentMenu;
        }

        public Menu.Menu CurrentMenu => _currentMenu;
        public Menu.Menu CreateOrFindMenu(string selectedOption)
        {
            switch (selectedOption)
            {
                case "Exit":
                    _command = new CommandExit(_currentMenu);
                    break;
                case "-":
                    _command = new CommandNull(_currentMenu);
                    break;
                case "Back":
                    _command = new CommandBack(_currentMenu);
                    break;
                case "Buy":
                    _command = new CommandBuy(_currentMenu);
                    break;
                case "Add to card":
                    _command = new CommandAddToCart(_currentMenu);
                    break;
                case "Remove from card":
                    _command = new CommandRemove(_currentMenu);
                    break;
                case "List of shops":
                    _command = new CommandShowShops(_currentMenu);
                    break;
                case "Shopping List":
                    _command = new CommandShowShoppingList(_currentMenu);
                    break;
            }

            _command = _currentMenu switch
            {
                ProductsMenu when _command is null => new CommandItemMenu(_currentMenu),
                ShopsMenu when _command is null => new CommandProductsMenu(_currentMenu),
                _ => _command
            };

            if (_command == null) throw new ShopException("Can't handle this choice");

            _currentMenu = _command.Execute();
            _command = null;
            return _currentMenu;
        }
    }
}