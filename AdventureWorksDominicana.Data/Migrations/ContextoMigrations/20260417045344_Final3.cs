using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdventureWorksDominicana.Data.Migrations.ContextoMigrations
{
    /// <inheritdoc />
    public partial class Final3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "MaxWage",
                schema: "HumanResources",
                table: "PayrollParameter",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<byte>(
                name: "PayFrequency",
                schema: "HumanResources",
                table: "Payroll",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AlterColumn<string>(
                name: "MaritalStatus",
                schema: "HumanResources",
                table: "Employee",
                type: "nchar(1)",
                fixedLength: true,
                maxLength: 1,
                nullable: false,
                comment: "M = Married, S = Single",
                oldClrType: typeof(string),
                oldType: "nchar",
                oldFixedLength: true,
                oldComment: "M = Married, S = Single");

            migrationBuilder.AlterColumn<string>(
                name: "JobTitle",
                schema: "HumanResources",
                table: "Employee",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "Work title such as Buyer or Sales Representative.",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Work title such as Buyer or Sales Representative.");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                schema: "HumanResources",
                table: "Employee",
                type: "nchar(1)",
                fixedLength: true,
                maxLength: 1,
                nullable: false,
                comment: "M = Male, F = Female",
                oldClrType: typeof(string),
                oldType: "nchar",
                oldFixedLength: true,
                oldComment: "M = Male, F = Female");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxWage",
                schema: "HumanResources",
                table: "PayrollParameter");

            migrationBuilder.DropColumn(
                name: "PayFrequency",
                schema: "HumanResources",
                table: "Payroll");

            migrationBuilder.AlterColumn<string>(
                name: "MaritalStatus",
                schema: "HumanResources",
                table: "Employee",
                type: "nchar",
                fixedLength: true,
                nullable: false,
                comment: "M = Married, S = Single",
                oldClrType: typeof(string),
                oldType: "nchar(1)",
                oldFixedLength: true,
                oldMaxLength: 1,
                oldComment: "M = Married, S = Single");

            migrationBuilder.AlterColumn<string>(
                name: "JobTitle",
                schema: "HumanResources",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Work title such as Buyer or Sales Representative.",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "Work title such as Buyer or Sales Representative.");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                schema: "HumanResources",
                table: "Employee",
                type: "nchar",
                fixedLength: true,
                nullable: false,
                comment: "M = Male, F = Female",
                oldClrType: typeof(string),
                oldType: "nchar(1)",
                oldFixedLength: true,
                oldMaxLength: 1,
                oldComment: "M = Male, F = Female");
        }
    }
}
