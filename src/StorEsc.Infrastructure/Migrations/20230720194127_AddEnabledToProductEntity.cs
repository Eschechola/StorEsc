using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorEsc.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEnableDtoProductEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                schema: "ste",
                table: "Product",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Enabled",
                schema: "ste",
                table: "Product");
        }
    }
}
