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
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorData", x => x.ID)
                        .Annotation("SqlServer:Clustered", false);
                });


            migrationBuilder.Sql(@"-- Create PartitionFunction
CREATE PARTITION FUNCTION [DailyPartitionFunction](datetimeoffset) AS RANGE RIGHT
FOR VALUES (N'2022-03-21T18:54:54.000', N'2022-03-21T18:54:55.000', N'2022-03-21T18:54:56.000', 
            N'2022-03-21T18:54:57.000')");

            migrationBuilder.Sql(@"-- Create PartitionScheme
CREATE PARTITION SCHEME DailyPartitionScheme
AS PARTITION DailyPartitionFunction ALL TO ([PRIMARY])");

            migrationBuilder.Sql(@"-- Create clustered index for partitioning
CREATE CLUSTERED INDEX IX_Timestamp
ON SensorData (Timestamp)
ON DailyPartitionScheme([timestamp])");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SensorData");
        }
    }
}
