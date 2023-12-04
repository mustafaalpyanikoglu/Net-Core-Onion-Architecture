using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class _Migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId1",
                table: "CustomerWarehouseCosts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerWarehouseCosts_CustomerId1",
                table: "CustomerWarehouseCosts",
                column: "CustomerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerWarehouseCosts_Customers_CustomerId1",
                table: "CustomerWarehouseCosts",
                column: "CustomerId1",
                principalTable: "Customers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerWarehouseCosts_Customers_CustomerId1",
                table: "CustomerWarehouseCosts");

            migrationBuilder.DropIndex(
                name: "IX_CustomerWarehouseCosts_CustomerId1",
                table: "CustomerWarehouseCosts");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                table: "CustomerWarehouseCosts");
        }
    }
}
