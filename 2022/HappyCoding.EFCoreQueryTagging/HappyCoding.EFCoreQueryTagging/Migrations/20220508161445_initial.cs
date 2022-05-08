using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HappyCoding.EFCoreQueryTagging.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Procedure",
                columns: table => new
                {
                    ID = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    CreateTimestampUtc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Field1 = table.Column<int>(type: "INTEGER", nullable: false),
                    Field2 = table.Column<int>(type: "INTEGER", nullable: false),
                    Field3 = table.Column<int>(type: "INTEGER", nullable: false),
                    Field4 = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Field5 = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Field6 = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Procedure", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProcedureActivity",
                columns: table => new
                {
                    ProcessActivityID = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProcessID = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    ActivityTimestampUtc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Field1 = table.Column<int>(type: "INTEGER", nullable: false),
                    Field2 = table.Column<int>(type: "INTEGER", nullable: false),
                    Field3 = table.Column<int>(type: "INTEGER", nullable: false),
                    Field4 = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Field5 = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Field6 = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcedureActivity", x => x.ProcessActivityID);
                    table.ForeignKey(
                        name: "FK_ProcedureActivity_Procedure_ProcessID",
                        column: x => x.ProcessID,
                        principalTable: "Procedure",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProcedureActivity_ProcessID",
                table: "ProcedureActivity",
                column: "ProcessID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcedureActivity");

            migrationBuilder.DropTable(
                name: "Procedure");
        }
    }
}
