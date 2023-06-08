using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication6.Data.Migrations
{
    public partial class addingoccupiedandisdonefields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "occupied",
                table: "RoomTimeSlot",
                newName: "Occupied");

            migrationBuilder.RenameColumn(
                name: "occupied",
                table: "PresenterTimeSlot",
                newName: "Occupied");

            migrationBuilder.RenameColumn(
                name: "occupied",
                table: "InvestorTimeSlot",
                newName: "Occupied");

            migrationBuilder.AddColumn<bool>(
                name: "IsDone",
                table: "Reservation",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDone",
                table: "Reservation");

            migrationBuilder.RenameColumn(
                name: "Occupied",
                table: "RoomTimeSlot",
                newName: "occupied");

            migrationBuilder.RenameColumn(
                name: "Occupied",
                table: "PresenterTimeSlot",
                newName: "occupied");

            migrationBuilder.RenameColumn(
                name: "Occupied",
                table: "InvestorTimeSlot",
                newName: "occupied");
        }
    }
}
