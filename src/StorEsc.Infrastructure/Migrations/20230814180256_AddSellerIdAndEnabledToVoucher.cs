using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorEsc.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSellerIdAndEnabledToVoucher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                schema: "ste",
                table: "Voucher",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SellerId",
                schema: "ste",
                table: "Voucher",
                type: "VARCHAR(36)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<bool>(
                name: "Enabled",
                schema: "ste",
                table: "Product",
                type: "BIT",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.CreateIndex(
                name: "IX_Voucher_SellerId",
                schema: "ste",
                table: "Voucher",
                column: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Voucher_Seller_SellerId",
                schema: "ste",
                table: "Voucher",
                column: "SellerId",
                principalSchema: "ste",
                principalTable: "Seller",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voucher_Seller_SellerId",
                schema: "ste",
                table: "Voucher");

            migrationBuilder.DropIndex(
                name: "IX_Voucher_SellerId",
                schema: "ste",
                table: "Voucher");

            migrationBuilder.DropColumn(
                name: "Enabled",
                schema: "ste",
                table: "Voucher");

            migrationBuilder.DropColumn(
                name: "SellerId",
                schema: "ste",
                table: "Voucher");

            migrationBuilder.AlterColumn<bool>(
                name: "Enabled",
                schema: "ste",
                table: "Product",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "BIT",
                oldDefaultValue: false);
        }
    }
}
