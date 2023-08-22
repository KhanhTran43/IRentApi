using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iRentApi.Migrations
{
    public partial class Warehouse_Ward_Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Ward",
                table: "Warehouses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ward",
                table: "Warehouses");
        }
    }
}
