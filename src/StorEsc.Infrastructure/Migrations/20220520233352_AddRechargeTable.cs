using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorEsc.Infrastructure.Migrations
{
    public partial class AddRechargeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recharge",
                schema: "ste",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WalletId = table.Column<string>(type: "VARCHAR(36)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(14,9)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recharge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recharge_Wallet_WalletId",
                        column: x => x.WalletId,
                        principalSchema: "ste",
                        principalTable: "Wallet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recharge_WalletId",
                schema: "ste",
                table: "Recharge",
                column: "WalletId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recharge");
        }
    }
}
