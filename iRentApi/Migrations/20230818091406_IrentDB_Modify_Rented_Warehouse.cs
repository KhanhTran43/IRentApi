using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iRentApi.Migrations
{
    public partial class IrentDB_Modify_Rented_Warehouse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentedWarehouses_Warehouses_WareHouseId",
                table: "RentedWarehouses");

            migrationBuilder.RenameColumn(
                name: "WareHouseId",
                table: "RentedWarehouses",
                newName: "WarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_RentedWarehouses_WareHouseId",
                table: "RentedWarehouses",
                newName: "IX_RentedWarehouses_WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_RentedWarehouses_Warehouses_WarehouseId",
                table: "RentedWarehouses",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentedWarehouses_Warehouses_WarehouseId",
                table: "RentedWarehouses");

            migrationBuilder.RenameColumn(
                name: "WarehouseId",
                table: "RentedWarehouses",
                newName: "WareHouseId");

            migrationBuilder.RenameIndex(
                name: "IX_RentedWarehouses_WarehouseId",
                table: "RentedWarehouses",
                newName: "IX_RentedWarehouses_WareHouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_RentedWarehouses_Warehouses_WareHouseId",
                table: "RentedWarehouses",
                column: "WareHouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
