using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorEsc.Infrastructure.Migrations
{
    public partial class RemovePaymentHashPropertyFromRechargeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentHash",
                schema: "ste",
                table: "Recharge");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentHash",
                schema: "ste",
                table: "Recharge",
                type: "VARCHAR(36)",
                nullable: false,
                defaultValue: "");
        }
    }
}
