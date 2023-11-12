using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HappyCoding.WebApiWithSqliteDb.Migrations
{
    /// <inheritdoc />
    public partial class AddedStreet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Persons",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Street",
                table: "Persons");
        }
    }
}
