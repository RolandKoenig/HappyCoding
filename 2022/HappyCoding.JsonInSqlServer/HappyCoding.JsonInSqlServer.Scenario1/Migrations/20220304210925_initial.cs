using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HappyCoding.JsonInSqlServer.Scenario1.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestingTable",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Timestamp1 = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Timestamp2 = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    JsonData = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestingTable", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestingTable");
        }
    }
}
