using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorEsc.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSellerFlux : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Seller_SellerId",
                schema: "ste",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Voucher_Seller_SellerId",
                schema: "ste",
                table: "Voucher");

            migrationBuilder.DropTable(
                name: "Seller",
                schema: "ste");

            migrationBuilder.DropIndex(
                name: "IX_Voucher_SellerId",
                schema: "ste",
                table: "Voucher");

            migrationBuilder.DropIndex(
                name: "IX_Product_SellerId",
                schema: "ste",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "SellerId",
                schema: "ste",
                table: "Voucher");

            migrationBuilder.DropColumn(
                name: "SellerId",
                schema: "ste",
                table: "Product");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SellerId",
                schema: "ste",
                table: "Voucher",
                type: "VARCHAR(36)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SellerId",
                schema: "ste",
                table: "Product",
                type: "VARCHAR(36)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Seller",
                schema: "ste",
                columns: table => new
                {
                    Id = table.Column<string>(type: "VARCHAR(36)", nullable: false),
                    WalletId = table.Column<string>(type: "VARCHAR(36)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(200)", nullable: false),
                    FirstName = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    LastName = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Password = table.Column<string>(type: "VARCHAR(120)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seller", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seller_Wallet_WalletId",
                        column: x => x.WalletId,
                        principalSchema: "ste",
                        principalTable: "Wallet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Voucher_SellerId",
                schema: "ste",
                table: "Voucher",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_SellerId",
                schema: "ste",
                table: "Product",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Seller_Id",
                schema: "ste",
                table: "Seller",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Seller_WalletId",
                schema: "ste",
                table: "Seller",
                column: "WalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Seller_SellerId",
                schema: "ste",
                table: "Product",
                column: "SellerId",
                principalSchema: "ste",
                principalTable: "Seller",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Voucher_Seller_SellerId",
                schema: "ste",
                table: "Voucher",
                column: "SellerId",
                principalSchema: "ste",
                principalTable: "Seller",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
