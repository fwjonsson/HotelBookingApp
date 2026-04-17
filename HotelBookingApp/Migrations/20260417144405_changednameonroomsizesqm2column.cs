using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelBookingApp.Migrations
{
    /// <inheritdoc />
    public partial class changednameonroomsizesqm2column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoomSizeSqM2",
                table: "Rooms",
                newName: "RoomSizeSqm2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoomSizeSqm2",
                table: "Rooms",
                newName: "RoomSizeSqM2");
        }
    }
}
