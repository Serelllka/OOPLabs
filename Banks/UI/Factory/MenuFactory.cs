using Banks.UI.Commands;

namespace Banks.UI.Factory
{
    public class MenuFactory
    {
        private Command _currentCommand;

        public MenuFactory(Menu.Menu currentMenu)
        {
            CurrentMenu = currentMenu;
        }

        public Menu.Menu CurrentMenu { get; private set; }

        public void CreateOrFindMenu()
        {
            _currentCommand = CurrentMenu.Show();
            CurrentMenu = _currentCommand.Execute();
        }
    }
}