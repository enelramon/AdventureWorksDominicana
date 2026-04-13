using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdventureWorksDominicana.Data.Migrations.ContextoMigrations
{
    /// <inheritdoc />
    public partial class Final2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BankAccountNumber",
                schema: "HumanResources",
                table: "Employee",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankAccountNumber",
                schema: "HumanResources",
                table: "Employee");
        }
    }
}
