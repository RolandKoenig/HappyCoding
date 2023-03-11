using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HappyCoding.EFCoreMultipleContextsInSameDb.Context1.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ctx1");

            migrationBuilder.CreateTable(
                name: "DataRows1",
                schema: "ctx1",
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
                    table.PrimaryKey("PK_DataRows1", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataRows1",
                schema: "ctx1");
        }
    }
}
