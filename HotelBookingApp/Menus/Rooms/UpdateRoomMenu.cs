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
        internal RoomEditorMenuOption Show(int roomNumber, RoomType roomType, int roomSizeSqm2, int extraBeds)
        {

            if (roomType == RoomType.Single || roomSizeSqm2 < 25)
            {
                _menu.SetChoices(new List<MenuChoice<RoomEditorMenuOption>>
            {
                new(RoomEditorMenuOption.RoomNumber, $"Room Number: {roomNumber}"),
                new(RoomEditorMenuOption.RoomType, $"Room Type: {roomType}"),
                new(RoomEditorMenuOption.RoomSizeSqm2, $"Room Size Sqm2: {roomSizeSqm2}"),
                new(RoomEditorMenuOption.ExtraBeds, $"Extra Beds Not Available For This Room Size."),
                new(RoomEditorMenuOption.Save, "Save")
            });
            }
            else if (roomType == RoomType.Double && roomSizeSqm2 < 30)
            {
                _menu.SetChoices(new List<MenuChoice<RoomEditorMenuOption>>
            {
                new(RoomEditorMenuOption.RoomNumber, $"Room Number: {roomNumber}"),
                new(RoomEditorMenuOption.RoomType, $"Room Type: {roomType}"),
                new(RoomEditorMenuOption.RoomSizeSqm2, $"Room Size Sqm2: {roomSizeSqm2}"),
                new(RoomEditorMenuOption.ExtraBeds, $"Extra Beds: {extraBeds} (Max 1) "),
                new(RoomEditorMenuOption.Save, "Save")
            });

            }
            else
            {
                _menu.SetChoices(new List<MenuChoice<RoomEditorMenuOption>>
            {
                new(RoomEditorMenuOption.RoomNumber, $"Room Number: {roomNumber}"),
                new(RoomEditorMenuOption.RoomType, $"Room Type: {roomType}"),
                new(RoomEditorMenuOption.RoomSizeSqm2, $"Room Size Sqm2: {roomSizeSqm2}"),
                new(RoomEditorMenuOption.ExtraBeds, $"Extra Beds: {extraBeds} (Max 2) "),
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
