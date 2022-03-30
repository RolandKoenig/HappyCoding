using Microsoft.EntityFrameworkCore.Migrations;

namespace HappyCoding.EFCoreFeatures.Migrations;

internal static class CustomMigrationActions
{
    public static void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("ALTER TABLE dbo.Testing ADD CalculationResult AS (CalculationA + CalculationB);");
    }

    public static void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("ALTER TABLE dbo.Testing REMOVE CalculationResult;");
    }
}
