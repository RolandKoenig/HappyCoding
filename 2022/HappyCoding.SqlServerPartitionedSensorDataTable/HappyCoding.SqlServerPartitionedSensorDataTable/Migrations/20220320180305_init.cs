using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HappyCoding.SqlServerPartitionedSensorDataTable.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SensorData",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Timestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    SensorName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SensorValue = table.Column<float>(type: "real", nullable: false)
                });
        constraints: table =>
        {
            table.PrimaryKey("PK_SensorData", x => x.ID);
        });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SensorData");
        }
    }
}
