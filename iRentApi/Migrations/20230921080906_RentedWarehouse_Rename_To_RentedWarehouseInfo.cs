using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iRentApi.Migrations
{
    public partial class RentedWarehouse_Rename_To_RentedWarehouseInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_RentedWarehouses_RentedWarehouseId",
                table: "Contract");

            migrationBuilder.DropTable(
                name: "RentedWarehouses");

            migrationBuilder.CreateTable(
                name: "RentedWarehouseInfos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RenterId = table.Column<long>(type: "bigint", nullable: true),
                    WarehouseId = table.Column<long>(type: "bigint", nullable: false),
                    RentedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConfirmDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Confirm = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Deposit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ContractBase64 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentedWarehouseInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentedWarehouseInfos_Users_RenterId",
                        column: x => x.RenterId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RentedWarehouseInfos_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RentedWarehouseInfos_RenterId",
                table: "RentedWarehouseInfos",
                column: "RenterId");

            migrationBuilder.CreateIndex(
                name: "IX_RentedWarehouseInfos_WarehouseId",
                table: "RentedWarehouseInfos",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_RentedWarehouseInfos_RentedWarehouseId",
                table: "Contract",
                column: "RentedWarehouseId",
                principalTable: "RentedWarehouseInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_RentedWarehouseInfos_RentedWarehouseId",
                table: "Contract");

            migrationBuilder.DropTable(
                name: "RentedWarehouseInfos");

            migrationBuilder.CreateTable(
                name: "RentedWarehouses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RenterId = table.Column<long>(type: "bigint", nullable: true),
                    WarehouseId = table.Column<long>(type: "bigint", nullable: false),
                    Confirm = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ConfirmDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ContractBase64 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deposit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RentedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentedWarehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentedWarehouses_Users_RenterId",
                        column: x => x.RenterId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RentedWarehouses_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RentedWarehouses_RenterId",
                table: "RentedWarehouses",
                column: "RenterId");

            migrationBuilder.CreateIndex(
                name: "IX_RentedWarehouses_WarehouseId",
                table: "RentedWarehouses",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_RentedWarehouses_RentedWarehouseId",
                table: "Contract",
                column: "RentedWarehouseId",
                principalTable: "RentedWarehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
