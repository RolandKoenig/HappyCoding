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
                    Timestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    SensorName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SensorValue = table.Column<float>(type: "real", nullable: false)
                });

            // 2022-03-21T18:54:54.3281670+01:00

            migrationBuilder.Sql(@"-- PartitionFunction erzeugen
CREATE PARTITION FUNCTION [DailyPartitionFunction](datetimeoffset) AS RANGE RIGHT
FOR VALUES (N'2022-03-21T18:54:54.000', N'2022-03-21T18:54:55.000', N'2022-03-21T18:54:56.000', 
            N'2022-03-21T18:54:57.000')");

            migrationBuilder.Sql(@"
CREATE PARTITION SCHEME DailyPartitionScheme
AS PARTITION DailyPartitionFunction ALL TO ([PRIMARY])");

            migrationBuilder.Sql(@"-- Primary key with partitioning
ALTER TABLE [dbo].[SensorData]
    ADD CONSTRAINT [PK_SensorData_Partition_timestamp]
PRIMARY KEY CLUSTERED (
    [timestamp] ASC
    )
    WITH (
        ONLINE = OFF -- This can be ONLINE = ON if using SQL Server Enterprise Edition 
    )
    ON DailyPartitionScheme([timestamp])");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SensorData");
        }
    }
}
