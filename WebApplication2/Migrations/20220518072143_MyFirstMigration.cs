using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication2.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaxFrees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Company = table.Column<string>(type: "text", nullable: true),
                    Country = table.Column<string>(type: "text", nullable: true),
                    VatRate = table.Column<int>(type: "integer", nullable: false),
                    DateOfPurchase = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    VatCode = table.Column<string>(type: "text", nullable: true),
                    DateTaxFreeRegistration = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxFrees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: true),
                    EmailAddress = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaxFrees");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
