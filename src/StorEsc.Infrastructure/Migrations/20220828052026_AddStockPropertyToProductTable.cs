using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorEsc.Infrastructure.Migrations
{
    public partial class AddStockPropertyToProductTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Stock",
                schema: "ste",
                table: "Product",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql("UPDATE [StorEsc].[ste].[Product] SET Stock = 0");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stock",
                schema: "ste",
                table: "Product");
        }
    }
}
