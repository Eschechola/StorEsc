using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorEsc.Infrastructure.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ste");

            migrationBuilder.CreateTable(
                name: "Wallet",
                schema: "ste",
                columns: table => new
                {
                    Id = table.Column<string>(type: "VARCHAR(36)", nullable: false),
                    Amount = table.Column<decimal>(type: "DECIMAL(14,9)", nullable: false, defaultValue: 0m),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                schema: "ste",
                columns: table => new
                {
                    Id = table.Column<string>(type: "VARCHAR(36)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    WalletId = table.Column<string>(type: "VARCHAR(36)", nullable: false),
                    FirstName = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    LastName = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(200)", nullable: false),
                    Password = table.Column<string>(type: "VARCHAR(80)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customer_Wallet_WalletId",
                        column: x => x.WalletId,
                        principalSchema: "ste",
                        principalTable: "Wallet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seller",
                schema: "ste",
                columns: table => new
                {
                    Id = table.Column<string>(type: "VARCHAR(36)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    WalletId = table.Column<string>(type: "VARCHAR(36)", nullable: false),
                    FirstName = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    LastName = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(200)", nullable: false),
                    Password = table.Column<string>(type: "VARCHAR(80)", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Order",
                schema: "ste",
                columns: table => new
                {
                    Id = table.Column<string>(type: "VARCHAR(36)", nullable: false),
                    CustomerId = table.Column<string>(type: "VARCHAR(36)", nullable: false),
                    TotalValue = table.Column<decimal>(type: "DECIMAL(19,4)", nullable: false),
                    IsPaid = table.Column<bool>(type: "BIT", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "ste",
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "ste",
                columns: table => new
                {
                    Id = table.Column<string>(type: "VARCHAR(36)", nullable: false),
                    SellerId = table.Column<string>(type: "VARCHAR(36)", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(200)", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(2000)", nullable: false),
                    Price = table.Column<decimal>(type: "DECIMAL(14,9)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Seller_SellerId",
                        column: x => x.SellerId,
                        principalSchema: "ste",
                        principalTable: "Seller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                schema: "ste",
                columns: table => new
                {
                    Id = table.Column<string>(type: "VARCHAR(36)", nullable: false),
                    OrderId = table.Column<string>(type: "VARCHAR(36)", nullable: false),
                    ProductId = table.Column<string>(type: "VARCHAR(36)", nullable: false),
                    Count = table.Column<int>(type: "INT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Order_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "ste",
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "ste",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_Id",
                schema: "ste",
                table: "Customer",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_WalletId",
                schema: "ste",
                table: "Customer",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                schema: "ste",
                table: "Order",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Id",
                schema: "ste",
                table: "Order",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_Id",
                schema: "ste",
                table: "OrderItem",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                schema: "ste",
                table: "OrderItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ProductId",
                schema: "ste",
                table: "OrderItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Id",
                schema: "ste",
                table: "Product",
                column: "Id",
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_Id",
                schema: "ste",
                table: "Wallet",
                column: "Id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItem",
                schema: "ste");

            migrationBuilder.DropTable(
                name: "Order",
                schema: "ste");

            migrationBuilder.DropTable(
                name: "Product",
                schema: "ste");

            migrationBuilder.DropTable(
                name: "Customer",
                schema: "ste");

            migrationBuilder.DropTable(
                name: "Seller",
                schema: "ste");

            migrationBuilder.DropTable(
                name: "Wallet",
                schema: "ste");
        }
    }
}
