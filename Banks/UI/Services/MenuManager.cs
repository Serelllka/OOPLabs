using Banks.BusinessLogic.Services;
using Banks.UI.Factory;
using Banks.UI.Menu;
using Spectre.Console;

namespace Banks.UI.Services
{
    public class MenuManager
    {
        private MenuFactory _factory;
        private CentralBank _centralBank;

        public MenuManager(CentralBank centralBank)
        {
            _centralBank = centralBank;
        }

        public void Start()
        {
            _factory = new MenuFactory(new CentralBankMenu(
                null,
                _centralBank));

            while (_factory.CurrentMenu != null)
            {
                AnsiConsole.Clear();
                _factory.CreateOrFindMenu();
            }
        }
    }
}