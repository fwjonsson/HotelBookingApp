using HotelBookingApp.Enums.Menus;
using HotelBookingApp.Utilities.Menus;

namespace HotelBookingApp.Menus
{
    internal class ActionMenu<T>
    {
        private readonly ConsoleMenu<ActionMenuOption> _menu;
        private readonly string _item;
        private readonly Func<T, string> _formatter;

        internal ActionMenu(string item, Func<T, string> formatter)
        {
            _item = item;
            _formatter = formatter;
            _menu = new ConsoleMenu<ActionMenuOption>("", new List<MenuChoice<ActionMenuOption>>());
        }
        internal ActionMenuOption Show(T type)
        {
            _menu.SetTitle($"{_item}\n\n{_formatter(type)}");
            _menu.SetChoices(new List<MenuChoice<ActionMenuOption>>
            {
                new(ActionMenuOption.Edit, "Edit"),
                new(ActionMenuOption.Delete, "Delete")
            });
            var result = _menu.Show();
            if (result.WasCancelled)
                return ActionMenuOption.Back;

            return result.Value;
        }
        internal void ResetIndex()
        {
            _menu.ResetIndex();
        }
    }
}
