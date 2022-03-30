using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HappyCoding.EFCoreFeatures.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Testing",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CalculationA = table.Column<int>(type: "int", nullable: false),
                    CalculationB = table.Column<int>(type: "int", nullable: false),
                    TagCollection = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Testing", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Parents",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parents", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Childs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ParentRowID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Childs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Childs_Parents_ParentRowID",
                        column: x => x.ParentRowID,
                        principalTable: "Parents",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Childs_ParentRowID",
                table: "Childs",
                column: "ParentRowID");

            CustomMigrationActions.Up(migrationBuilder);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            CustomMigrationActions.Down(migrationBuilder);

            migrationBuilder.DropTable(
                name: "Childs");

            migrationBuilder.DropTable(
                name: "Parents");

            migrationBuilder.DropTable(
                name: "Testing");
        }
    }
}
