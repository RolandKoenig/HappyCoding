using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HappyCoding.EFCoreMultipleContextsInSameDb.Context2.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ctx2");

            migrationBuilder.CreateTable(
                name: "DataRows2",
                schema: "ctx2",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dummy1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Dummy2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Dummy3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataRows2", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataRows2",
                schema: "ctx2");
        }
    }
}
