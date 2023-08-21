using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iRentApi.Migrations
{
    public partial class Users_Update_Model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "Users",
                newName: "Dob");

            migrationBuilder.AddColumn<string>(
                name: "Ioc",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ioc",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Dob",
                table: "Users",
                newName: "DateOfBirth");
        }
    }
}
