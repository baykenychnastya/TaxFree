using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication2.Migrations
{
    public partial class addUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "TaxFrees",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TaxFrees");
        }
    }
}
