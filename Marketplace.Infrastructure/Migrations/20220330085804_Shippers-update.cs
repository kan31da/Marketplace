using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marketplace.Infrastructure.Data.Migrations
{
    public partial class Shippersupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shipper_Orders_OrderId",
                table: "Shipper");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shipper",
                table: "Shipper");

            migrationBuilder.RenameTable(
                name: "Shipper",
                newName: "Shippers");

            migrationBuilder.RenameIndex(
                name: "IX_Shipper_OrderId",
                table: "Shippers",
                newName: "IX_Shippers_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shippers",
                table: "Shippers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shippers_Orders_OrderId",
                table: "Shippers",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shippers_Orders_OrderId",
                table: "Shippers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shippers",
                table: "Shippers");

            migrationBuilder.RenameTable(
                name: "Shippers",
                newName: "Shipper");

            migrationBuilder.RenameIndex(
                name: "IX_Shippers_OrderId",
                table: "Shipper",
                newName: "IX_Shipper_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shipper",
                table: "Shipper",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shipper_Orders_OrderId",
                table: "Shipper",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
