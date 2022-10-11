using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorEsc.Infrastructure.Migrations
{
    public partial class UpdateMonetaryTypesOnRechargeAndWallet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                schema: "ste",
                table: "Wallet",
                type: "DECIMAL(19,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,9)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                schema: "ste",
                table: "Recharge",
                type: "DECIMAL(19,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,9)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                schema: "ste",
                table: "Product",
                type: "DECIMAL(19,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,9)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                schema: "ste",
                table: "Wallet",
                type: "DECIMAL(14,9)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(19,4)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                schema: "ste",
                table: "Recharge",
                type: "DECIMAL(14,9)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(19,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                schema: "ste",
                table: "Product",
                type: "DECIMAL(14,9)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(19,4)");
        }
    }
}
