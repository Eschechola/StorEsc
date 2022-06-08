using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorEsc.Infrastructure.Migrations
{
    public partial class AddVoucherTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VoucherId",
                schema: "ste",
                table: "Order",
                type: "VARCHAR(36)",
                nullable: false,
                defaultValue: "");
            
            migrationBuilder.CreateTable(
                name: "Voucher",
                schema: "ste",
                columns: table => new
                {
                    Id = table.Column<string>(type: "VARCHAR(36)", nullable: false),
                    Code = table.Column<string>(type: "VARCHAR(80)", nullable: false),
                    ValueDiscount = table.Column<decimal>(type: "DECIMAL(14,9)", nullable: true),
                    IsPercentageDiscount = table.Column<bool>(type: "BIT", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voucher", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_VoucherId",
                schema: "ste",
                table: "Order",
                column: "VoucherId");
            
            migrationBuilder.CreateIndex(
                name: "IX_Voucher_Id",
                schema: "ste",
                table: "Voucher",
                column: "Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Voucher_VoucherId",
                schema: "ste",
                table: "Order",
                column: "VoucherId",
                principalSchema: "ste",
                principalTable: "Voucher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Voucher_VoucherId",
                schema: "ste",
                table: "Order");

            migrationBuilder.DropTable(
                name: "Voucher",
                schema: "ste");

            migrationBuilder.DropIndex(
                name: "IX_Order_VoucherId",
                schema: "ste",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "VoucherId",
                schema: "ste",
                table: "Order"); 
        }
    }
}
