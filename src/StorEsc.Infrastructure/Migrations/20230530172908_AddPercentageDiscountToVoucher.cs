using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorEsc.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPercentageDiscountToVoucher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PercentageDiscount",
                schema: "ste",
                table: "Voucher",
                type: "DECIMAL(14,9)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PercentageDiscount",
                schema: "ste",
                table: "Voucher");
        }
    }
}
