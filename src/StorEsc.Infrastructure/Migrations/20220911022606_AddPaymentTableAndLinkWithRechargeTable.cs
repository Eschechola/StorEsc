using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorEsc.Infrastructure.Migrations
{
    public partial class AddPaymentTableAndLinkWithRechargeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentId",
                schema: "ste",
                table: "Recharge",
                type: "VARCHAR(36)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Payment",
                schema: "ste",
                columns: table => new
                {
                    Id = table.Column<string>(type: "VARCHAR(36)", nullable: false),
                    Hash = table.Column<string>(type: "VARCHAR(72)", nullable: false),
                    IsPaid = table.Column<bool>(type: "BIT", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recharge_PaymentId",
                schema: "ste",
                table: "Recharge",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_Id",
                schema: "ste",
                table: "Payment",
                column: "Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Recharge_Payment_PaymentId",
                schema: "ste",
                table: "Recharge",
                column: "PaymentId",
                principalSchema: "ste",
                principalTable: "Payment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recharge_Payment_PaymentId",
                schema: "ste",
                table: "Recharge");

            migrationBuilder.DropTable(
                name: "Payment",
                schema: "ste");

            migrationBuilder.DropIndex(
                name: "IX_Recharge_PaymentId",
                schema: "ste",
                table: "Recharge");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                schema: "ste",
                table: "Recharge");
        }
    }
}
