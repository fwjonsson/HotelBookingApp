using HotelBookingApp.Enums.Menus.Bookings;
using HotelBookingApp.Models;
using HotelBookingApp.Utilities.Menus;

namespace HotelBookingApp.Menus.Bookings
{
    internal class CreateBookingMenu
    {
        private readonly ConsoleMenu<BookingEditorMenuOption> _menu;

        internal CreateBookingMenu()
        {
            _menu = new ConsoleMenu<BookingEditorMenuOption>(
                "--Create New Booking--", new List<MenuChoice<BookingEditorMenuOption>>());
        }
        internal BookingEditorMenuOption Show(Customer? customer, Room? room, DateOnly? startDate, DateOnly? endDate)
        {
            _menu.SetChoices(new List<MenuChoice<BookingEditorMenuOption>>
            {
                new(BookingEditorMenuOption.CustomerId, $"Customer ID: {customer?.FullName.ToString() ?? "Not set"}"),
                new(BookingEditorMenuOption.StartDate, $"Start Date (yyyy-MM-dd): {startDate?.ToString("yyyy-MM-dd") ?? "Not set"}"),
                new(BookingEditorMenuOption.EndDate, $"End Date (yyyy-MM-dd): {endDate?.ToString("yyyy-MM-dd")?? "Not set"}"),
                new(BookingEditorMenuOption.RoomId, room is null
                ? $"Room Number: Select dates first"
                : $"Room Number: {room.RoomNumber} Extra Beds: {room.ExtraBeds}"),
                new(BookingEditorMenuOption.Save, $"Save")
            });
            var result = _menu.Show();
            if (result.WasCancelled)
                return BookingEditorMenuOption.Cancel;
            return result.Value;

        }
        internal void ResetMenu() => _menu.ResetMenu();
    }
}
