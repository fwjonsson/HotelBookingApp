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
            _menu = new ConsoleMenu<RoomEditorMenuOption>("--Update Room--", new List<MenuChoice<RoomEditorMenuOption>>());
        }
        internal RoomEditorMenuOption Show(int roomNumber, RoomType roomType, int extraBeds)
        {
            _menu.SetChoices(new List<MenuChoice<RoomEditorMenuOption>>
            {
                new(RoomEditorMenuOption.RoomNumber, $"Room number ({roomNumber})"),
                new(RoomEditorMenuOption.RoomType, $"Room type ({roomType.ToString()})"),
                new(RoomEditorMenuOption.ExtraBeds, $"Extra beds ({extraBeds})"),
                new(RoomEditorMenuOption.Save, "Save")
            });
            var result = _menu.Show();
            if (result.WasCancelled)
                return RoomEditorMenuOption.Cancel;
            return result.Value;
        }
        internal void ResetIndex()
        {
            _menu.ResetIndex();
        }
    }
}
