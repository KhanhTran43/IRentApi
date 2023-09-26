using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iRentApi.Migrations
{
    public partial class RentedWarehouse_Extend_Renting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RentingExtends",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RentedWarehouseInfoId = table.Column<long>(type: "bigint", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    ExtendDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentingExtends", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentingExtends_RentedWarehouseInfos_RentedWarehouseInfoId",
                        column: x => x.RentedWarehouseInfoId,
                        principalTable: "RentedWarehouseInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RentingExtends_RentedWarehouseInfoId",
                table: "RentingExtends",
                column: "RentedWarehouseInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RentingExtends");
        }
    }
}
