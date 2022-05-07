using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HappyCoding.EFIncludePerformance.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Process",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreateTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Field1 = table.Column<int>(type: "int", nullable: false),
                    Field2 = table.Column<int>(type: "int", nullable: false),
                    Field3 = table.Column<int>(type: "int", nullable: false),
                    Field4 = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Field5 = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Field6 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Process", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProcessActivity",
                columns: table => new
                {
                    ProcessActivityID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessID = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Field1 = table.Column<int>(type: "int", nullable: false),
                    Field2 = table.Column<int>(type: "int", nullable: false),
                    Field3 = table.Column<int>(type: "int", nullable: false),
                    Field4 = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Field5 = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Field6 = table.Column<int>(type: "int", nullable: false),
                    Field7 = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessActivity", x => x.ProcessActivityID);
                    table.ForeignKey(
                        name: "FK_ProcessActivity_Process_ProcessID",
                        column: x => x.ProcessID,
                        principalTable: "Process",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProcessActivity_ProcessID",
                table: "ProcessActivity",
                column: "ProcessID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessActivity");

            migrationBuilder.DropTable(
                name: "Process");
        }
    }
}
