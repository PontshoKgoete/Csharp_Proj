using Microsoft.EntityFrameworkCore.Migrations;

namespace Zaka.Core.API.Persistence.Migrations
{
    public partial class AddedSecretKeycolumntoAccounttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SecretKey",
                table: "Accounts",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecretKey",
                table: "Accounts");
        }
    }
}
