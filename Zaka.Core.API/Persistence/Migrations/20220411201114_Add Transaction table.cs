using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Zaka.Core.API.Persistence.Migrations
{
    public partial class AddTransactiontable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TransactionReference = table.Column<Guid>(type: "uuid", nullable: false),
                    TransactionTypeCode = table.Column<string>(type: "text", nullable: true),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    FromWallet = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    ToWallet = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    FromPhone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    ToPhone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    TransactionStatus = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    VoucherNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
