using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelAPI.Migrations
{
    /// <inheritdoc />
    public partial class updateBookTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_booking_AspNetUsers_UserId",
                table: "booking");

            migrationBuilder.DropForeignKey(
                name: "FK_booking_Room_RoomId",
                table: "booking");

            migrationBuilder.DropIndex(
                name: "IX_booking_UserId",
                table: "booking");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "booking");

            migrationBuilder.RenameColumn(
                name: "RoomId",
                table: "booking",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_booking_RoomId",
                table: "booking",
                newName: "IX_booking_OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_booking_Order_OrderId",
                table: "booking",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_booking_Order_OrderId",
                table: "booking");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "booking",
                newName: "RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_booking_OrderId",
                table: "booking",
                newName: "IX_booking_RoomId");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "booking",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_booking_UserId",
                table: "booking",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_booking_AspNetUsers_UserId",
                table: "booking",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_booking_Room_RoomId",
                table: "booking",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
