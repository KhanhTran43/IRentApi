using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iRentApi.Migrations
{
    public partial class IrentDB_Update_RentedWarehouse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "RentedWarehouses");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "RentedWarehouses",
                newName: "RentedDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "RentedWarehouses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "RentedWarehouses");

            migrationBuilder.RenameColumn(
                name: "RentedDate",
                table: "RentedWarehouses",
                newName: "Date");

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "RentedWarehouses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
