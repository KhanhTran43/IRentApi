using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iRentApi.Migrations
{
    public partial class RentedWarehosue_Deposit_Payment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DepositPayment",
                table: "RentedWarehouseInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepositPayment",
                table: "RentedWarehouseInfos");
        }
    }
}
