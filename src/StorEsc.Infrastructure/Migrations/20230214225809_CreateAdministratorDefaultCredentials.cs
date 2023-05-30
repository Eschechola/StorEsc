using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorEsc.Infrastructure.Migrations
{
    public partial class CreateAdministratorDefaultCredentials : Migration
    {
        private const string DEFAULT_EMAIL = "admin@storesc.com";
        private const string DEFAULT_PASSWORD = "!sT0ReSC@4dM1n_";
        
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var id = Guid.NewGuid().ToString();
            
            var query = string.Format(@"
                INSERT INTO [ste].[Administrator]
                VALUES
                (
                    '{0}',
                    GETDATE(),
                    GETDATE(),
                    'ADMINISTRATOR',
                    'STORESC',
                    '{1}',
                    '{2}',
                    '00000000-0000-0000-0000-000000000000'
                )
            ", id, DEFAULT_EMAIL, DEFAULT_PASSWORD);
            
            migrationBuilder.Sql(query);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var query = string.Format(@"
                DELETE FROM [ste].[Administrator]
                WHERE Email = '{0}'
            ", DEFAULT_EMAIL);
            
            migrationBuilder.Sql(query);
        }
    }
}
