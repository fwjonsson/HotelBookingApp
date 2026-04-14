using HotelBookingApp.Utilities.Menus;

namespace HotelBookingApp.Menus
{
    internal class SelectObjectMenu<T> where T : class
    {
        private readonly ConsoleMenu<T> _menu;
        private readonly Func<T, string> _formatter;

        internal SelectObjectMenu(Func<T, string> formatter)
        {
            _menu = new ConsoleMenu<T>("", new List<MenuChoice<T>>());
            _formatter = formatter;
        }
        internal T? Show(List<T> items, string title)
        {
            if (items.Count == 0)
                return null;

            _menu.SetTitle(title);

            var choices = items
                .Select(item => new MenuChoice<T>(item, _formatter(item)))
                .ToList();

            _menu.SetChoices(choices);

            var result = _menu.Show();

            if (result.WasCancelled)
                return null;

            return result.Value;
        }
        internal void ResetIndex()
        {
            _menu.ResetIndex();
        }
    }
}
