using HotelBookingApp.Utilities.Menus;

namespace HotelBookingApp.Menus
{
    internal class SelectObjectMenu<T> where T : class
    {
        private readonly ConsoleMenu<T> _menu;

        internal SelectObjectMenu()
        {
            _menu = new ConsoleMenu<T>("", new List<MenuChoice<T>>());

        }
        internal T? Show(List<T> items, Func<T, string> formatter, string title)
        {
            if (items.Count == 0)
                return null;

            _menu.SetTitle(title);

            var choices = items
                .Select(item => new MenuChoice<T>(item, formatter(item)))
                .ToList();

            _menu.SetChoices(choices);

            var result = _menu.Show();

            if (result.WasCancelled)
                return null;

            return result.Value;
        }
        internal void ResetMenu() => _menu.ResetMenu();
    }
}
