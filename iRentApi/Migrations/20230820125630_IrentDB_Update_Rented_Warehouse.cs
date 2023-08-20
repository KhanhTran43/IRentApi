using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iRentApi.Migrations
{
    public partial class IrentDB_Update_Rented_Warehouse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "Rented",
                table: "Warehouses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Warehouses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Rented",
                table: "Warehouses",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
