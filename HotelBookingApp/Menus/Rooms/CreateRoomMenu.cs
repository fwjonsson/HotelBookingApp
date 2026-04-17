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
                new(RoomEditorMenuOption.RoomSizeSqm2, $"Room Size Sqm2: {roomSizeSqm2?.ToString() ?? "Not set"}"),
                new(RoomEditorMenuOption.ExtraBeds, $"Extra Beds: Must Enter Room Type And Size First."),
                new(RoomEditorMenuOption.Save, "Save")
            });
            }
            else if (roomType == RoomType.Single || roomSizeSqm2 < 25)
            {
                _menu.SetChoices(new List<MenuChoice<RoomEditorMenuOption>>
            {
                new(RoomEditorMenuOption.RoomNumber, $"Room Number: {roomNumber?.ToString() ?? "Not set" }"),
                new(RoomEditorMenuOption.RoomType, $"Room Type: {roomType}"),
                new(RoomEditorMenuOption.RoomSizeSqm2, $"Room Size Sqm2: {roomSizeSqm2?.ToString() ?? "Not set"}"),
                new(RoomEditorMenuOption.ExtraBeds, $"Extra Beds Not Available For This Room Size."),
                new(RoomEditorMenuOption.Save, "Save")
            });
            }
            else if (roomType == RoomType.Double && roomSizeSqm2 < 30)
            {
                _menu.SetChoices(new List<MenuChoice<RoomEditorMenuOption>>
            {
                new(RoomEditorMenuOption.RoomNumber, $"Room Number: {roomNumber?.ToString() ?? "Not set" }"),
                new(RoomEditorMenuOption.RoomType, $"Room Type: {roomType}"),
                new(RoomEditorMenuOption.RoomSizeSqm2, $"Room Size Sqm2: {roomSizeSqm2?.ToString() ?? "Not set"}"),
                new(RoomEditorMenuOption.ExtraBeds, $"Extra Beds: {extraBeds?.ToString() ?? "Not set"} (Max 1) "),
                new(RoomEditorMenuOption.Save, "Save")
            });

            }
            else
            {
                _menu.SetChoices(new List<MenuChoice<RoomEditorMenuOption>>
            {
                new(RoomEditorMenuOption.RoomNumber, $"Room Number: {roomNumber?.ToString() ?? "Not set" }"),
                new(RoomEditorMenuOption.RoomType, $"Room Type: {roomType}"),
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
