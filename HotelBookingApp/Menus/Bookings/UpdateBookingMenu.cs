using HotelBookingApp.Enums.Menus.Bookings;
using HotelBookingApp.Utilities.Menus;

namespace HotelBookingApp.Menus.Bookings
{
    internal class UpdateBookingMenu
    {
        private readonly ConsoleMenu<BookingEditorMenuOption> _menu;
        internal UpdateBookingMenu()
        {
            _menu = new ConsoleMenu<BookingEditorMenuOption>(
                "--Update Booking--"
                , new List<MenuChoice<BookingEditorMenuOption>>());
        }
        internal BookingEditorMenuOption Show(DateOnly startDate, DateOnly endDate, int roomId, int customerId)
        {
            _menu.SetChoices(new List<MenuChoice<BookingEditorMenuOption>>
            {
                new(BookingEditorMenuOption.StartDate, $"Start Date ({startDate:yyyy-MM-dd})"),
                new(BookingEditorMenuOption.EndDate, $"End Date ({endDate:yyyy-MM-dd})"),
                new(BookingEditorMenuOption.RoomId, $"Room ID ({roomId})"),
                new(BookingEditorMenuOption.CustomerId, $"Change Customer By Customer ID ({customerId})"),
                new(BookingEditorMenuOption.Save, "Save")
            });
            var result = _menu.Show();
            if (result.WasCancelled)
                return BookingEditorMenuOption.Cancel;
            return result.Value;
        }
        internal void ResetIndex()
        {
            _menu.ResetIndex();
        }
    }
}
