using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorEsc.Infrastructure.Migrations
{
    public partial class IncreasePasswordLenght : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                schema: "ste",
                table: "Seller",
                type: "VARCHAR(120)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(80)");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                schema: "ste",
                table: "Customer",
                type: "VARCHAR(120)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(80)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                schema: "ste",
                table: "Seller",
                type: "VARCHAR(80)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(120)");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                schema: "ste",
                table: "Customer",
                type: "VARCHAR(80)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(120)");
        }
    }
}
