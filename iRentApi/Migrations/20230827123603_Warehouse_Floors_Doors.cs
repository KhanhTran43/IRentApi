using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iRentApi.Migrations
{
    public partial class Warehouse_Floors_Doors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Doors",
                table: "Warehouses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Floors",
                table: "Warehouses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Doors",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "Floors",
                table: "Warehouses");
        }
    }
}
