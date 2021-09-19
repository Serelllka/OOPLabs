using Spectre.Console;

namespace Shops.UI.Menu
{
    public interface IMenu
    {
        void Show();
        IMenu GenerateNextMenu();

        void UpdateTable();
    }
}