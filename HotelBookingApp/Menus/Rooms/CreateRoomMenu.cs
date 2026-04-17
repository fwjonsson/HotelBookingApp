using HotelBookingApp.Enums;
using HotelBookingApp.Enums.Menus.Rooms;
using HotelBookingApp.Utilities.Menus;

namespace HotelBookingApp.Menus.Rooms
{
    internal class CreateRoomMenu
    {
        private readonly ConsoleMenu<RoomEditorMenuOption> _menu;

        internal CreateRoomMenu()
        {
            _menu = new ConsoleMenu<RoomEditorMenuOption>("--Create New Room--", new List<MenuChoice<RoomEditorMenuOption>>());
        }
        internal RoomEditorMenuOption Show(int? roomNumber, RoomType? roomType, int? roomSizeSqm2, int? extraBeds)
        {
            if (roomType == null)
            {
                _menu.SetChoices(new List<MenuChoice<RoomEditorMenuOption>>
            {
                new(RoomEditorMenuOption.RoomNumber, $"Room Number: {roomNumber?.ToString() ?? "Not set" }"),
                new(RoomEditorMenuOption.RoomType, $"Room Type: Not set"),
                new(RoomEditorMenuOption.RoomSizeSqm2, $"Room Size Sqn2: Enter Room Type First."),
                new(RoomEditorMenuOption.ExtraBeds, $"Extra Beds: Enter Room Type And Size."),
                new(RoomEditorMenuOption.Save, "Save")
            });
            }
            else if (roomType == RoomType.Single)
            {
                _menu.SetChoices(new List<MenuChoice<RoomEditorMenuOption>>
            {
                new(RoomEditorMenuOption.RoomNumber, $"Room Number: {roomNumber?.ToString() ?? "Not set" }"),
                new(RoomEditorMenuOption.RoomType, $"Room Type: Single"),
                new(RoomEditorMenuOption.RoomSizeSqm2, $"Room Size Sqm2: {roomSizeSqm2?.ToString() ?? "Not set"}"),
                new(RoomEditorMenuOption.ExtraBeds, $"Extra Beds: Not Available For Single Rooms."),
                new(RoomEditorMenuOption.Save, "Save")
            });
            }
            else if (roomType == RoomType.Double)
            {
                _menu.SetChoices(new List<MenuChoice<RoomEditorMenuOption>>
            {
                new(RoomEditorMenuOption.RoomNumber, $"Room Number: {roomNumber?.ToString() ?? "Not set" }"),
                new(RoomEditorMenuOption.RoomType, $"Room Type: Double"),
                new(RoomEditorMenuOption.RoomSizeSqm2, $"Room Size Sqm2: {roomSizeSqm2?.ToString() ?? "Not set"}"),
                new(RoomEditorMenuOption.ExtraBeds, $"Extra Beds: {extraBeds?.ToString() ?? "Not set"} (Max 2) "),
                new(RoomEditorMenuOption.Save, "Save")
            });

            }
            var result = _menu.Show();
            if (result.WasCancelled)
                return RoomEditorMenuOption.Cancel;
            return result.Value;
        }
        internal void ResetMenu() => _menu.ResetMenu();
    }
}
