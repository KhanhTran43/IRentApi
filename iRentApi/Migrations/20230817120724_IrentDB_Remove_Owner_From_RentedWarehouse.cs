using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iRentApi.Migrations
{
    public partial class IrentDB_Remove_Owner_From_RentedWarehouse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentedWarehouses_Users_OwnerId",
                table: "RentedWarehouses");

            migrationBuilder.DropIndex(
                name: "IX_RentedWarehouses_OwnerId",
                table: "RentedWarehouses");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "RentedWarehouses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OwnerId",
                table: "RentedWarehouses",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RentedWarehouses_OwnerId",
                table: "RentedWarehouses",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_RentedWarehouses_Users_OwnerId",
                table: "RentedWarehouses",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
