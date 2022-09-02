using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorEsc.Infrastructure.Migrations
{
    public partial class AddFullTextSearch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                sql: "CREATE FULLTEXT CATALOG StorEscFullTextSearchCatalog AS DEFAULT;",
                suppressTransaction: true);

            migrationBuilder.Sql(
                sql: @"CREATE FULLTEXT INDEX ON [StorEsc].[ste].[Product](Name)
                       KEY INDEX PK_Product ON StorEscFullTextSearchCatalog
                       WITH STOPLIST = OFF, CHANGE_TRACKING AUTO;",
                suppressTransaction: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                sql: "DROP FULLTEXT INDEX ON [StorEsc].[ste].[Product];",
                suppressTransaction: true);
            
            migrationBuilder.Sql(
                sql: "DROP FULLTEXT CATALOG StorEscFullTextSearchCatalog;",
                suppressTransaction: true);
        }
    }
}
