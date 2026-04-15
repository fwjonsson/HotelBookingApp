using HotelBookingApp.Enums;
using HotelBookingApp.Enums.Menus.Rooms;
using HotelBookingApp.Utilities.Menus;

namespace HotelBookingApp.Menus.Rooms
{
    internal class UpdateRoomMenu
    {
        private readonly ConsoleMenu<RoomEditorMenuOption> _menu;

        internal UpdateRoomMenu()
        {
            _menu = new ConsoleMenu<RoomEditorMenuOption>("--Edit Room--", new List<MenuChoice<RoomEditorMenuOption>>());
        }
        internal RoomEditorMenuOption Show(int roomNumber, RoomType roomType, int extraBeds)
        {
            _menu.SetChoices(new List<MenuChoice<RoomEditorMenuOption>>
            {
                new(RoomEditorMenuOption.RoomNumber, $"Room Number: {roomNumber}"),
                new(RoomEditorMenuOption.RoomType, $"Room Type: {roomType.ToString()}"),
                new(RoomEditorMenuOption.ExtraBeds, $"Extra Beds: {extraBeds}"),
                new(RoomEditorMenuOption.Save, "Save")
            });
            var result = _menu.Show();
            if (result.WasCancelled)
                return RoomEditorMenuOption.Cancel;
            return result.Value;
        }
        internal void ResetMenu() => _menu.ResetMenu();
    }
}
