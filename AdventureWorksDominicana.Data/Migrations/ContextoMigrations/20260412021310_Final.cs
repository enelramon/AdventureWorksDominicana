using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AdventureWorksDominicana.Data.Migrations.ContextoMigrations
{
    /// <inheritdoc />
    public partial class Final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Person");

            migrationBuilder.EnsureSchema(
                name: "Production");

            migrationBuilder.EnsureSchema(
                name: "Sales");

            migrationBuilder.EnsureSchema(
                name: "HumanResources");

            migrationBuilder.EnsureSchema(
                name: "Purchasing");

            migrationBuilder.CreateTable(
                name: "AddressType",
                schema: "Person",
                columns: table => new
                {
                    AddressTypeID = table.Column<int>(type: "int", nullable: false, comment: "Primary key for AddressType records.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Address type description. For example, Billing, Home, or Shipping."),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())", comment: "ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.")
                        .Annotation("Relational:DefaultConstraintName", "DF_AddressType_rowguid"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_AddressType_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressType_AddressTypeID", x => x.AddressTypeID);
                },
                comment: "Types of addresses stored in the Address table. ");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AWBuildVersion",
                columns: table => new
                {
                    SystemInformationID = table.Column<byte>(type: "tinyint", nullable: false, comment: "Primary key for AWBuildVersion records.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatabaseVersion = table.Column<string>(name: "Database Version", type: "nvarchar(25)", maxLength: 25, nullable: false, comment: "Version number of the database in 9.yy.mm.dd.00 format."),
                    VersionDate = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Date and time the record was last updated."),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_AWBuildVersion_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AWBuildVersion_SystemInformationID", x => x.SystemInformationID);
                },
                comment: "Current version number of the AdventureWorks 2016 sample database. ");

            migrationBuilder.CreateTable(
                name: "BusinessEntity",
                schema: "Person",
                columns: table => new
                {
                    BusinessEntityID = table.Column<int>(type: "int", nullable: false, comment: "Primary key for all customers, vendors, and employees.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())", comment: "ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.")
                        .Annotation("Relational:DefaultConstraintName", "DF_BusinessEntity_rowguid"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_BusinessEntity_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessEntity_BusinessEntityID", x => x.BusinessEntityID);
                },
                comment: "Source of the ID that connects vendors, customers, and employees with address and contact information.");

            migrationBuilder.CreateTable(
                name: "ContactType",
                schema: "Person",
                columns: table => new
                {
                    ContactTypeID = table.Column<int>(type: "int", nullable: false, comment: "Primary key for ContactType records.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Contact type description."),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_ContactType_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactType_ContactTypeID", x => x.ContactTypeID);
                },
                comment: "Lookup table containing the types of business entity contacts.");

            migrationBuilder.CreateTable(
                name: "CountryRegion",
                schema: "Person",
                columns: table => new
                {
                    CountryRegionCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false, comment: "ISO standard code for countries and regions."),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Country or region name."),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_CountryRegion_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryRegion_CountryRegionCode", x => x.CountryRegionCode);
                },
                comment: "Lookup table containing the ISO standard codes for countries and regions.");

            migrationBuilder.CreateTable(
                name: "CreditCard",
                schema: "Sales",
                columns: table => new
                {
                    CreditCardID = table.Column<int>(type: "int", nullable: false, comment: "Primary key for CreditCard records.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Credit card name."),
                    CardNumber = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false, comment: "Credit card number."),
                    ExpMonth = table.Column<byte>(type: "tinyint", nullable: false, comment: "Credit card expiration month."),
                    ExpYear = table.Column<short>(type: "smallint", nullable: false, comment: "Credit card expiration year."),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_CreditCard_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCard_CreditCardID", x => x.CreditCardID);
                },
                comment: "Customer credit card information.");

            migrationBuilder.CreateTable(
                name: "Culture",
                schema: "Production",
                columns: table => new
                {
                    CultureID = table.Column<string>(type: "nchar(6)", fixedLength: true, maxLength: 6, nullable: false, comment: "Primary key for Culture records."),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Culture description."),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_Culture_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Culture_CultureID", x => x.CultureID);
                },
                comment: "Lookup table containing the languages in which some AdventureWorks data is stored.");

            migrationBuilder.CreateTable(
                name: "Currency",
                schema: "Sales",
                columns: table => new
                {
                    CurrencyCode = table.Column<string>(type: "nchar(3)", fixedLength: true, maxLength: 3, nullable: false, comment: "The ISO code for the Currency."),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Currency name."),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_Currency_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency_CurrencyCode", x => x.CurrencyCode);
                },
                comment: "Lookup table containing standard ISO currencies.");

            migrationBuilder.CreateTable(
                name: "DatabaseLog",
                columns: table => new
                {
                    DatabaseLogID = table.Column<int>(type: "int", nullable: false, comment: "Primary key for DatabaseLog records.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostTime = table.Column<DateTime>(type: "datetime", nullable: false, comment: "The date and time the DDL change occurred."),
                    DatabaseUser = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: "The user who implemented the DDL change."),
                    Event = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: "The type of DDL statement that was executed."),
                    Schema = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true, comment: "The schema to which the changed object belongs."),
                    Object = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true, comment: "The object that was changed by the DDL statment."),
                    TSQL = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "The exact Transact-SQL statement that was executed."),
                    XmlEvent = table.Column<string>(type: "xml", nullable: false, comment: "The raw XML data generated by database trigger.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatabaseLog_DatabaseLogID", x => x.DatabaseLogID)
                        .Annotation("SqlServer:Clustered", false);
                },
                comment: "Audit table tracking all DDL changes made to the AdventureWorks database. Data is captured by the database trigger ddlDatabaseTriggerLog.");

            migrationBuilder.CreateTable(
                name: "Department",
                schema: "HumanResources",
                columns: table => new
                {
                    DepartmentID = table.Column<short>(type: "smallint", nullable: false, comment: "Primary key for Department records.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Name of the department."),
                    GroupName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Name of the group to which the department belongs."),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_Department_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department_DepartmentID", x => x.DepartmentID);
                },
                comment: "Lookup table containing the departments within the Adventure Works Cycles company.");

            migrationBuilder.CreateTable(
                name: "ErrorLog",
                columns: table => new
                {
                    ErrorLogID = table.Column<int>(type: "int", nullable: false, comment: "Primary key for ErrorLog records.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ErrorTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "The date and time at which the error occurred.")
                        .Annotation("Relational:DefaultConstraintName", "DF_ErrorLog_ErrorTime"),
                    UserName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: "The user who executed the batch in which the error occurred."),
                    ErrorNumber = table.Column<int>(type: "int", nullable: false, comment: "The error number of the error that occurred."),
                    ErrorSeverity = table.Column<int>(type: "int", nullable: true, comment: "The severity of the error that occurred."),
                    ErrorState = table.Column<int>(type: "int", nullable: true, comment: "The state number of the error that occurred."),
                    ErrorProcedure = table.Column<string>(type: "nvarchar(126)", maxLength: 126, nullable: true, comment: "The name of the stored procedure or trigger where the error occurred."),
                    ErrorLine = table.Column<int>(type: "int", nullable: true, comment: "The line number at which the error occurred."),
                    ErrorMessage = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false, comment: "The message text of the error that occurred.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorLog_ErrorLogID", x => x.ErrorLogID);
                },
                comment: "Audit table tracking errors in the the AdventureWorks database that are caught by the CATCH block of a TRY...CATCH construct. Data is inserted by stored procedure dbo.uspLogError when it is executed from inside the CATCH block of a TRY...CATCH construct.");

            migrationBuilder.CreateTable(
                name: "Illustration",
                schema: "Production",
                columns: table => new
                {
                    IllustrationID = table.Column<int>(type: "int", nullable: false, comment: "Primary key for Illustration records.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Diagram = table.Column<string>(type: "xml", nullable: true, comment: "Illustrations used in manufacturing instructions. Stored as XML."),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_Illustration_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Illustration_IllustrationID", x => x.IllustrationID);
                },
                comment: "Bicycle assembly diagrams.");

            migrationBuilder.CreateTable(
                name: "Location",
                schema: "Production",
                columns: table => new
                {
                    LocationID = table.Column<short>(type: "smallint", nullable: false, comment: "Primary key for Location records.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Location description."),
                    CostRate = table.Column<decimal>(type: "smallmoney", nullable: false, comment: "Standard hourly cost of the manufacturing location."),
                    Availability = table.Column<decimal>(type: "decimal(8,2)", nullable: false, comment: "Work capacity (in hours) of the manufacturing location."),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_Location_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location_LocationID", x => x.LocationID);
                },
                comment: "Product inventory and manufacturing locations.");

            migrationBuilder.CreateTable(
                name: "Payroll",
                schema: "HumanResources",
                columns: table => new
                {
                    PayrollId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PeriodStartDate = table.Column<DateTime>(type: "date", nullable: false),
                    PeriodEndDate = table.Column<DateTime>(type: "date", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "date", nullable: true),
                    Status = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payroll", x => x.PayrollId);
                });

            migrationBuilder.CreateTable(
                name: "PayrollParameter",
                schema: "HumanResources",
                columns: table => new
                {
                    PayrollParameterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    SfsPct = table.Column<decimal>(type: "decimal(5,4)", nullable: false),
                    AfpPct = table.Column<decimal>(type: "decimal(5,4)", nullable: false),
                    MinimumWage = table.Column<decimal>(type: "money", nullable: false),
                    IsrAnnualExemption = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayrollParameter", x => x.PayrollParameterId);
                });

            migrationBuilder.CreateTable(
                name: "PhoneNumberType",
                schema: "Person",
                columns: table => new
                {
                    PhoneNumberTypeID = table.Column<int>(type: "int", nullable: false, comment: "Primary key for telephone number type records.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Name of the telephone number type"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_PhoneNumberType_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumberType_PhoneNumberTypeID", x => x.PhoneNumberTypeID);
                },
                comment: "Type of phone number of a person.");

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                schema: "Production",
                columns: table => new
                {
                    ProductCategoryID = table.Column<int>(type: "int", nullable: false, comment: "Primary key for ProductCategory records.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Category description."),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())", comment: "ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.")
                        .Annotation("Relational:DefaultConstraintName", "DF_ProductCategory_rowguid"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_ProductCategory_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory_ProductCategoryID", x => x.ProductCategoryID);
                },
                comment: "High-level product categorization.");

            migrationBuilder.CreateTable(
                name: "ProductDescription",
                schema: "Production",
                columns: table => new
                {
                    ProductDescriptionID = table.Column<int>(type: "int", nullable: false, comment: "Primary key for ProductDescription records.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false, comment: "Description of the product."),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())", comment: "ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.")
                        .Annotation("Relational:DefaultConstraintName", "DF_ProductDescription_rowguid"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_ProductDescription_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDescription_ProductDescriptionID", x => x.ProductDescriptionID);
                },
                comment: "Product descriptions in several languages.");

            migrationBuilder.CreateTable(
                name: "ProductModel",
                schema: "Production",
                columns: table => new
                {
                    ProductModelID = table.Column<int>(type: "int", nullable: false, comment: "Primary key for ProductModel records.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Product model description."),
                    CatalogDescription = table.Column<string>(type: "xml", nullable: true, comment: "Detailed product catalog information in xml format."),
                    Instructions = table.Column<string>(type: "xml", nullable: true, comment: "Manufacturing instructions in xml format."),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())", comment: "ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.")
                        .Annotation("Relational:DefaultConstraintName", "DF_ProductModel_rowguid"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_ProductModel_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductModel_ProductModelID", x => x.ProductModelID);
                },
                comment: "Product model classification.");

            migrationBuilder.CreateTable(
                name: "ProductPhoto",
                schema: "Production",
                columns: table => new
                {
                    ProductPhotoID = table.Column<int>(type: "int", nullable: false, comment: "Primary key for ProductPhoto records.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThumbNailPhoto = table.Column<byte[]>(type: "varbinary(max)", nullable: true, comment: "Small image of the product."),
                    ThumbnailPhotoFileName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "Small image file name."),
                    LargePhoto = table.Column<byte[]>(type: "varbinary(max)", nullable: true, comment: "Large image of the product."),
                    LargePhotoFileName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "Large image file name."),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_ProductPhoto_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPhoto_ProductPhotoID", x => x.ProductPhotoID);
                },
                comment: "Product images.");

            migrationBuilder.CreateTable(
                name: "SalesReason",
                schema: "Sales",
                columns: table => new
                {
                    SalesReasonID = table.Column<int>(type: "int", nullable: false, comment: "Primary key for SalesReason records.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Sales reason description."),
                    ReasonType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Category the sales reason belongs to."),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_SalesReason_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesReason_SalesReasonID", x => x.SalesReasonID);
                },
                comment: "Lookup table of customer purchase reasons.");

            migrationBuilder.CreateTable(
                name: "ScrapReason",
                schema: "Production",
                columns: table => new
                {
                    ScrapReasonID = table.Column<short>(type: "smallint", nullable: false, comment: "Primary key for ScrapReason records.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Failure description."),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_ScrapReason_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScrapReason_ScrapReasonID", x => x.ScrapReasonID);
                },
                comment: "Manufacturing failure reasons lookup table.");

            migrationBuilder.CreateTable(
                name: "Shift",
                schema: "HumanResources",
                columns: table => new
                {
                    ShiftID = table.Column<byte>(type: "tinyint", nullable: false, comment: "Primary key for Shift records.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Shift description."),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false, comment: "Shift start time."),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false, comment: "Shift end time."),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_Shift_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shift_ShiftID", x => x.ShiftID);
                },
                comment: "Work shift lookup table.");

            migrationBuilder.CreateTable(
                name: "ShipMethod",
                schema: "Purchasing",
                columns: table => new
                {
                    ShipMethodID = table.Column<int>(type: "int", nullable: false, comment: "Primary key for ShipMethod records.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Shipping company name."),
                    ShipBase = table.Column<decimal>(type: "money", nullable: false, comment: "Minimum shipping charge."),
                    ShipRate = table.Column<decimal>(type: "money", nullable: false, comment: "Shipping charge per pound."),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())", comment: "ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.")
                        .Annotation("Relational:DefaultConstraintName", "DF_ShipMethod_rowguid"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_ShipMethod_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipMethod_ShipMethodID", x => x.ShipMethodID);
                },
                comment: "Shipping company lookup table.");

            migrationBuilder.CreateTable(
                name: "SpecialOffer",
                schema: "Sales",
                columns: table => new
                {
                    SpecialOfferID = table.Column<int>(type: "int", nullable: false, comment: "Primary key for SpecialOffer records.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Discount description."),
                    DiscountPct = table.Column<decimal>(type: "smallmoney", nullable: false, comment: "Discount precentage."),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Discount type category."),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Group the discount applies to such as Reseller or Customer."),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Discount start date."),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Discount end date."),
                    MinQty = table.Column<int>(type: "int", nullable: false, comment: "Minimum discount percent allowed."),
                    MaxQty = table.Column<int>(type: "int", nullable: true, comment: "Maximum discount percent allowed."),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())", comment: "ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.")
                        .Annotation("Relational:DefaultConstraintName", "DF_SpecialOffer_rowguid"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_SpecialOffer_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialOffer_SpecialOfferID", x => x.SpecialOfferID);
                },
                comment: "Sale discounts lookup table.");

            migrationBuilder.CreateTable(
                name: "TransactionHistoryArchive",
                schema: "Production",
                columns: table => new
                {
                    TransactionID = table.Column<int>(type: "int", nullable: false, comment: "Primary key for TransactionHistoryArchive records."),
                    ProductID = table.Column<int>(type: "int", nullable: false, comment: "Product identification number. Foreign key to Product.ProductID."),
                    ReferenceOrderID = table.Column<int>(type: "int", nullable: false, comment: "Purchase order, sales order, or work order identification number."),
                    ReferenceOrderLineID = table.Column<int>(type: "int", nullable: false, comment: "Line number associated with the purchase order, sales order, or work order."),
                    TransactionDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time of the transaction.")
                        .Annotation("Relational:DefaultConstraintName", "DF_TransactionHistoryArchive_TransactionDate"),
                    TransactionType = table.Column<string>(type: "nchar(1)", fixedLength: true, maxLength: 1, nullable: false, comment: "W = Work Order, S = Sales Order, P = Purchase Order"),
                    Quantity = table.Column<int>(type: "int", nullable: false, comment: "Product quantity."),
                    ActualCost = table.Column<decimal>(type: "money", nullable: false, comment: "Product cost."),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_TransactionHistoryArchive_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionHistoryArchive_TransactionID", x => x.TransactionID);
                },
                comment: "Transactions for previous years.");

            migrationBuilder.CreateTable(
                name: "UnitMeasure",
                schema: "Production",
                columns: table => new
                {
                    UnitMeasureCode = table.Column<string>(type: "nchar(3)", fixedLength: true, maxLength: 3, nullable: false, comment: "Primary key."),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Unit of measure description."),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_UnitMeasure_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitMeasure_UnitMeasureCode", x => x.UnitMeasureCode);
                },
                comment: "Unit of measure lookup table.");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserPasskeys",
                columns: table => new
                {
                    CredentialId = table.Column<byte[]>(type: "varbinary(1024)", maxLength: 1024, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserPasskeys", x => x.CredentialId);
                    table.ForeignKey(
                        name: "FK_AspNetUserPasskeys_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                schema: "Person",
                columns: table => new
                {
                    BusinessEntityID = table.Column<int>(type: "int", nullable: false, comment: "Primary key for Person records."),
                    PersonType = table.Column<string>(type: "nchar(2)", fixedLength: true, maxLength: 2, nullable: false, comment: "Primary type of person: SC = Store Contact, IN = Individual (retail) customer, SP = Sales person, EM = Employee (non-sales), VC = Vendor contact, GC = General contact"),
                    NameStyle = table.Column<bool>(type: "bit", nullable: false, comment: "0 = The data in FirstName and LastName are stored in western style (first name, last name) order.  1 = Eastern style (last name, first name) order."),
                    Title = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true, comment: "A courtesy title. For example, Mr. or Ms."),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "First name of the person."),
                    MiddleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "Middle name or middle initial of the person."),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Last name of the person."),
                    Suffix = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true, comment: "Surname suffix. For example, Sr. or Jr."),
                    EmailPromotion = table.Column<int>(type: "int", nullable: false, comment: "0 = Contact does not wish to receive e-mail promotions, 1 = Contact does wish to receive e-mail promotions from AdventureWorks, 2 = Contact does wish to receive e-mail promotions from AdventureWorks and selected partners. "),
                    AdditionalContactInfo = table.Column<string>(type: "xml", nullable: true, comment: "Additional contact information about the person stored in xml format. "),
                    Demographics = table.Column<string>(type: "xml", nullable: true, comment: "Personal information such as hobbies, and income collected from online shoppers. Used for sales analysis."),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())", comment: "ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.")
                        .Annotation("Relational:DefaultConstraintName", "DF_Person_rowguid"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_Person_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person_BusinessEntityID", x => x.BusinessEntityID);
                    table.ForeignKey(
                        name: "FK_Person_BusinessEntity_BusinessEntityID",
                        column: x => x.BusinessEntityID,
                        principalSchema: "Person",
                        principalTable: "BusinessEntity",
                        principalColumn: "BusinessEntityID");
                },
                comment: "Human beings involved with AdventureWorks: employees, customer contacts, and vendor contacts.");

            migrationBuilder.CreateTable(
                name: "Vendor",
                schema: "Purchasing",
                columns: table => new
                {
                    BusinessEntityID = table.Column<int>(type: "int", nullable: false, comment: "Primary key for Vendor records.  Foreign key to BusinessEntity.BusinessEntityID"),
                    AccountNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false, comment: "Vendor account (identification) number."),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Company name."),
                    CreditRating = table.Column<byte>(type: "tinyint", nullable: false, comment: "1 = Superior, 2 = Excellent, 3 = Above average, 4 = Average, 5 = Below average"),
                    PreferredVendorStatus = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "0 = Do not use if another vendor is available. 1 = Preferred over other vendors supplying the same product.")
                        .Annotation("Relational:DefaultConstraintName", "DF_Vendor_PreferredVendorStatus"),
                    ActiveFlag = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "0 = Vendor no longer used. 1 = Vendor is actively used.")
                        .Annotation("Relational:DefaultConstraintName", "DF_Vendor_ActiveFlag"),
                    PurchasingWebServiceURL = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true, comment: "Vendor URL."),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_Vendor_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendor_BusinessEntityID", x => x.BusinessEntityID);
                    table.ForeignKey(
                        name: "FK_Vendor_BusinessEntity_BusinessEntityID",
                        column: x => x.BusinessEntityID,
                        principalSchema: "Person",
                        principalTable: "BusinessEntity",
                        principalColumn: "BusinessEntityID");
                },
                comment: "Companies from whom Adventure Works Cycles purchases parts or other goods.");

            migrationBuilder.CreateTable(
                name: "SalesTerritory",
                schema: "Sales",
                columns: table => new
                {
                    TerritoryID = table.Column<int>(type: "int", nullable: false, comment: "Primary key for SalesTerritory records.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Sales territory description"),
                    CountryRegionCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false, comment: "ISO standard country or region code. Foreign key to CountryRegion.CountryRegionCode. "),
                    Group = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Geographic area to which the sales territory belong."),
                    SalesYTD = table.Column<decimal>(type: "money", nullable: false, comment: "Sales in the territory year to date."),
                    SalesLastYear = table.Column<decimal>(type: "money", nullable: false, comment: "Sales in the territory the previous year."),
                    CostYTD = table.Column<decimal>(type: "money", nullable: false, comment: "Business costs in the territory year to date."),
                    CostLastYear = table.Column<decimal>(type: "money", nullable: false, comment: "Business costs in the territory the previous year."),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())", comment: "ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.")
                        .Annotation("Relational:DefaultConstraintName", "DF_SalesTerritory_rowguid"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_SalesTerritory_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesTerritory_TerritoryID", x => x.TerritoryID);
                    table.ForeignKey(
                        name: "FK_SalesTerritory_CountryRegion_CountryRegionCode",
                        column: x => x.CountryRegionCode,
                        principalSchema: "Person",
                        principalTable: "CountryRegion",
                        principalColumn: "CountryRegionCode");
                },
                comment: "Sales territory lookup table.");

            migrationBuilder.CreateTable(
                name: "CountryRegionCurrency",
                schema: "Sales",
                columns: table => new
                {
                    CountryRegionCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false, comment: "ISO code for countries and regions. Foreign key to CountryRegion.CountryRegionCode."),
                    CurrencyCode = table.Column<string>(type: "nchar(3)", fixedLength: true, maxLength: 3, nullable: false, comment: "ISO standard currency code. Foreign key to Currency.CurrencyCode."),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_CountryRegionCurrency_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryRegionCurrency_CountryRegionCode_CurrencyCode", x => new { x.CountryRegionCode, x.CurrencyCode });
                    table.ForeignKey(
                        name: "FK_CountryRegionCurrency_CountryRegion_CountryRegionCode",
                        column: x => x.CountryRegionCode,
                        principalSchema: "Person",
                        principalTable: "CountryRegion",
                        principalColumn: "CountryRegionCode");
                    table.ForeignKey(
                        name: "FK_CountryRegionCurrency_Currency_CurrencyCode",
                        column: x => x.CurrencyCode,
                        principalSchema: "Sales",
                        principalTable: "Currency",
                        principalColumn: "CurrencyCode");
                },
                comment: "Cross-reference table mapping ISO currency codes to a country or region.");

            migrationBuilder.CreateTable(
                name: "CurrencyRate",
                schema: "Sales",
                columns: table => new
                {
                    CurrencyRateID = table.Column<int>(type: "int", nullable: false, comment: "Primary key for CurrencyRate records.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyRateDate = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Date and time the exchange rate was obtained."),
                    FromCurrencyCode = table.Column<string>(type: "nchar(3)", fixedLength: true, maxLength: 3, nullable: false, comment: "Exchange rate was converted from this currency code."),
                    ToCurrencyCode = table.Column<string>(type: "nchar(3)", fixedLength: true, maxLength: 3, nullable: false, comment: "Exchange rate was converted to this currency code."),
                    AverageRate = table.Column<decimal>(type: "money", nullable: false, comment: "Average exchange rate for the day."),
                    EndOfDayRate = table.Column<decimal>(type: "money", nullable: false, comment: "Final exchange rate for the day."),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_CurrencyRate_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyRate_CurrencyRateID", x => x.CurrencyRateID);
                    table.ForeignKey(
                        name: "FK_CurrencyRate_Currency_FromCurrencyCode",
                        column: x => x.FromCurrencyCode,
                        principalSchema: "Sales",
                        principalTable: "Currency",
                        principalColumn: "CurrencyCode");
                    table.ForeignKey(
                        name: "FK_CurrencyRate_Currency_ToCurrencyCode",
                        column: x => x.ToCurrencyCode,
                        principalSchema: "Sales",
                        principalTable: "Currency",
                        principalColumn: "CurrencyCode");
                },
                comment: "Currency exchange rates.");

            migrationBuilder.CreateTable(
                name: "ProductSubcategory",
                schema: "Production",
                columns: table => new
                {
                    ProductSubcategoryID = table.Column<int>(type: "int", nullable: false, comment: "Primary key for ProductSubcategory records.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductCategoryID = table.Column<int>(type: "int", nullable: false, comment: "Product category identification number. Foreign key to ProductCategory.ProductCategoryID."),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Subcategory description."),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())", comment: "ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.")
                        .Annotation("Relational:DefaultConstraintName", "DF_ProductSubcategory_rowguid"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_ProductSubcategory_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSubcategory_ProductSubcategoryID", x => x.ProductSubcategoryID);
                    table.ForeignKey(
                        name: "FK_ProductSubcategory_ProductCategory_ProductCategoryID",
                        column: x => x.ProductCategoryID,
                        principalSchema: "Production",
                        principalTable: "ProductCategory",
                        principalColumn: "ProductCategoryID");
                },
                comment: "Product subcategories. See ProductCategory table.");

            migrationBuilder.CreateTable(
                name: "ProductModelIllustration",
                schema: "Production",
                columns: table => new
                {
                    ProductModelID = table.Column<int>(type: "int", nullable: false, comment: "Primary key. Foreign key to ProductModel.ProductModelID."),
                    IllustrationID = table.Column<int>(type: "int", nullable: false, comment: "Primary key. Foreign key to Illustration.IllustrationID."),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_ProductModelIllustration_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductModelIllustration_ProductModelID_IllustrationID", x => new { x.ProductModelID, x.IllustrationID });
                    table.ForeignKey(
                        name: "FK_ProductModelIllustration_Illustration_IllustrationID",
                        column: x => x.IllustrationID,
                        principalSchema: "Production",
                        principalTable: "Illustration",
                        principalColumn: "IllustrationID");
                    table.ForeignKey(
                        name: "FK_ProductModelIllustration_ProductModel_ProductModelID",
                        column: x => x.ProductModelID,
                        principalSchema: "Production",
                        principalTable: "ProductModel",
                        principalColumn: "ProductModelID");
                },
                comment: "Cross-reference table mapping product models and illustrations.");

            migrationBuilder.CreateTable(
                name: "ProductModelProductDescriptionCulture",
                schema: "Production",
                columns: table => new
                {
                    ProductModelID = table.Column<int>(type: "int", nullable: false, comment: "Primary key. Foreign key to ProductModel.ProductModelID."),
                    ProductDescriptionID = table.Column<int>(type: "int", nullable: false, comment: "Primary key. Foreign key to ProductDescription.ProductDescriptionID."),
                    CultureID = table.Column<string>(type: "nchar(6)", fixedLength: true, maxLength: 6, nullable: false, comment: "Culture identification number. Foreign key to Culture.CultureID."),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_ProductModelProductDescriptionCulture_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductModelProductDescriptionCulture_ProductModelID_ProductDescriptionID_CultureID", x => new { x.ProductModelID, x.ProductDescriptionID, x.CultureID });
                    table.ForeignKey(
                        name: "FK_ProductModelProductDescriptionCulture_Culture_CultureID",
                        column: x => x.CultureID,
                        principalSchema: "Production",
                        principalTable: "Culture",
                        principalColumn: "CultureID");
                    table.ForeignKey(
                        name: "FK_ProductModelProductDescriptionCulture_ProductDescription_ProductDescriptionID",
                        column: x => x.ProductDescriptionID,
                        principalSchema: "Production",
                        principalTable: "ProductDescription",
                        principalColumn: "ProductDescriptionID");
                    table.ForeignKey(
                        name: "FK_ProductModelProductDescriptionCulture_ProductModel_ProductModelID",
                        column: x => x.ProductModelID,
                        principalSchema: "Production",
                        principalTable: "ProductModel",
                        principalColumn: "ProductModelID");
                },
                comment: "Cross-reference table mapping product descriptions and the language the description is written in.");

            migrationBuilder.CreateTable(
                name: "BusinessEntityContact",
                schema: "Person",
                columns: table => new
                {
                    BusinessEntityID = table.Column<int>(type: "int", nullable: false, comment: "Primary key. Foreign key to BusinessEntity.BusinessEntityID."),
                    PersonID = table.Column<int>(type: "int", nullable: false, comment: "Primary key. Foreign key to Person.BusinessEntityID."),
                    ContactTypeID = table.Column<int>(type: "int", nullable: false, comment: "Primary key.  Foreign key to ContactType.ContactTypeID."),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())", comment: "ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.")
                        .Annotation("Relational:DefaultConstraintName", "DF_BusinessEntityContact_rowguid"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_BusinessEntityContact_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessEntityContact_BusinessEntityID_PersonID_ContactTypeID", x => new { x.BusinessEntityID, x.PersonID, x.ContactTypeID });
                    table.ForeignKey(
                        name: "FK_BusinessEntityContact_BusinessEntity_BusinessEntityID",
                        column: x => x.BusinessEntityID,
                        principalSchema: "Person",
                        principalTable: "BusinessEntity",
                        principalColumn: "BusinessEntityID");
                    table.ForeignKey(
                        name: "FK_BusinessEntityContact_ContactType_ContactTypeID",
                        column: x => x.ContactTypeID,
                        principalSchema: "Person",
                        principalTable: "ContactType",
                        principalColumn: "ContactTypeID");
                    table.ForeignKey(
                        name: "FK_BusinessEntityContact_Person_PersonID",
                        column: x => x.PersonID,
                        principalSchema: "Person",
                        principalTable: "Person",
                        principalColumn: "BusinessEntityID");
                },
                comment: "Cross-reference table mapping stores, vendors, and employees to people");

            migrationBuilder.CreateTable(
                name: "EmailAddress",
                schema: "Person",
                columns: table => new
                {
                    BusinessEntityID = table.Column<int>(type: "int", nullable: false, comment: "Primary key. Person associated with this email address.  Foreign key to Person.BusinessEntityID"),
                    EmailAddressID = table.Column<int>(type: "int", nullable: false, comment: "Primary key. ID of this email address.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "E-mail address for the person."),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())", comment: "ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.")
                        .Annotation("Relational:DefaultConstraintName", "DF_EmailAddress_rowguid"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_EmailAddress_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailAddress_BusinessEntityID_EmailAddressID", x => new { x.BusinessEntityID, x.EmailAddressID });
                    table.ForeignKey(
                        name: "FK_EmailAddress_Person_BusinessEntityID",
                        column: x => x.BusinessEntityID,
                        principalSchema: "Person",
                        principalTable: "Person",
                        principalColumn: "BusinessEntityID");
                },
                comment: "Where to send a person email.");

            migrationBuilder.CreateTable(
                name: "Employee",
                schema: "HumanResources",
                columns: table => new
                {
                    BusinessEntityID = table.Column<int>(type: "int", nullable: false, comment: "Primary key for Employee records.  Foreign key to BusinessEntity.BusinessEntityID."),
                    NationalIDNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false, comment: "Unique national identification number such as a social security number."),
                    LoginID = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false, comment: "Network login."),
                    OrganizationLevel = table.Column<short>(type: "smallint", nullable: true, computedColumnSql: "([OrganizationNode].[GetLevel]())", stored: false, comment: "The depth of the employee in the corporate hierarchy."),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Work title such as Buyer or Sales Representative."),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false, comment: "Date of birth."),
                    MaritalStatus = table.Column<string>(type: "nchar", fixedLength: true, nullable: false, comment: "M = Married, S = Single"),
                    Gender = table.Column<string>(type: "nchar", fixedLength: true, nullable: false, comment: "M = Male, F = Female"),
                    HireDate = table.Column<DateOnly>(type: "date", nullable: false, comment: "Employee hired on this date."),
                    SalariedFlag = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "Job classification. 0 = Hourly, not exempt from collective bargaining. 1 = Salaried, exempt from collective bargaining.")
                        .Annotation("Relational:DefaultConstraintName", "DF_Employee_SalariedFlag"),
                    VacationHours = table.Column<short>(type: "smallint", nullable: false, comment: "Number of available vacation hours."),
                    SickLeaveHours = table.Column<short>(type: "smallint", nullable: false, comment: "Number of available sick leave hours."),
                    CurrentFlag = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "0 = Inactive, 1 = Active")
                        .Annotation("Relational:DefaultConstraintName", "DF_Employee_CurrentFlag"),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())", comment: "ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.")
                        .Annotation("Relational:DefaultConstraintName", "DF_Employee_rowguid"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_Employee_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee_BusinessEntityID", x => x.BusinessEntityID);
                    table.ForeignKey(
                        name: "FK_Employee_Person_BusinessEntityID",
                        column: x => x.BusinessEntityID,
                        principalSchema: "Person",
                        principalTable: "Person",
                        principalColumn: "BusinessEntityID");
                },
                comment: "Employee information such as salary, department, and title.");

            migrationBuilder.CreateTable(
                name: "Password",
                schema: "Person",
                columns: table => new
                {
                    BusinessEntityID = table.Column<int>(type: "int", nullable: false),
                    PasswordHash = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false, comment: "Password for the e-mail account."),
                    PasswordSalt = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false, comment: "Random value concatenated with the password string before the password is hashed."),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())", comment: "ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.")
                        .Annotation("Relational:DefaultConstraintName", "DF_Password_rowguid"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_Password_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Password_BusinessEntityID", x => x.BusinessEntityID);
                    table.ForeignKey(
                        name: "FK_Password_Person_BusinessEntityID",
                        column: x => x.BusinessEntityID,
                        principalSchema: "Person",
                        principalTable: "Person",
                        principalColumn: "BusinessEntityID");
                },
                comment: "One way hashed authentication information");

            migrationBuilder.CreateTable(
                name: "PersonCreditCard",
                schema: "Sales",
                columns: table => new
                {
                    BusinessEntityID = table.Column<int>(type: "int", nullable: false, comment: "Business entity identification number. Foreign key to Person.BusinessEntityID."),
                    CreditCardID = table.Column<int>(type: "int", nullable: false, comment: "Credit card identification number. Foreign key to CreditCard.CreditCardID."),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_PersonCreditCard_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonCreditCard_BusinessEntityID_CreditCardID", x => new { x.BusinessEntityID, x.CreditCardID });
                    table.ForeignKey(
                        name: "FK_PersonCreditCard_CreditCard_CreditCardID",
                        column: x => x.CreditCardID,
                        principalSchema: "Sales",
                        principalTable: "CreditCard",
                        principalColumn: "CreditCardID");
                    table.ForeignKey(
                        name: "FK_PersonCreditCard_Person_BusinessEntityID",
                        column: x => x.BusinessEntityID,
                        principalSchema: "Person",
                        principalTable: "Person",
                        principalColumn: "BusinessEntityID");
                },
                comment: "Cross-reference table mapping people to their credit card information in the CreditCard table. ");

            migrationBuilder.CreateTable(
                name: "PersonPhone",
                schema: "Person",
                columns: table => new
                {
                    BusinessEntityID = table.Column<int>(type: "int", nullable: false, comment: "Business entity identification number. Foreign key to Person.BusinessEntityID."),
                    PhoneNumber = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false, comment: "Telephone number identification number."),
                    PhoneNumberTypeID = table.Column<int>(type: "int", nullable: false, comment: "Kind of phone number. Foreign key to PhoneNumberType.PhoneNumberTypeID."),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_PersonPhone_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonPhone_BusinessEntityID_PhoneNumber_PhoneNumberTypeID", x => new { x.BusinessEntityID, x.PhoneNumber, x.PhoneNumberTypeID });
                    table.ForeignKey(
                        name: "FK_PersonPhone_Person_BusinessEntityID",
                        column: x => x.BusinessEntityID,
                        principalSchema: "Person",
                        principalTable: "Person",
                        principalColumn: "BusinessEntityID");
                    table.ForeignKey(
                        name: "FK_PersonPhone_PhoneNumberType_PhoneNumberTypeID",
                        column: x => x.PhoneNumberTypeID,
                        principalSchema: "Person",
                        principalTable: "PhoneNumberType",
                        principalColumn: "PhoneNumberTypeID");
                },
                comment: "Telephone number and type of a person.");

            migrationBuilder.CreateTable(
                name: "StateProvince",
                schema: "Person",
                columns: table => new
                {
                    StateProvinceID = table.Column<int>(type: "int", nullable: false, comment: "Primary key for StateProvince records.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateProvinceCode = table.Column<string>(type: "nchar(3)", fixedLength: true, maxLength: 3, nullable: false, comment: "ISO standard state or province code."),
                    CountryRegionCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false, comment: "ISO standard country or region code. Foreign key to CountryRegion.CountryRegionCode. "),
                    IsOnlyStateProvinceFlag = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "0 = StateProvinceCode exists. 1 = StateProvinceCode unavailable, using CountryRegionCode.")
                        .Annotation("Relational:DefaultConstraintName", "DF_StateProvince_IsOnlyStateProvinceFlag"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "State or province description."),
                    TerritoryID = table.Column<int>(type: "int", nullable: false, comment: "ID of the territory in which the state or province is located. Foreign key to SalesTerritory.SalesTerritoryID."),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())", comment: "ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.")
                        .Annotation("Relational:DefaultConstraintName", "DF_StateProvince_rowguid"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_StateProvince_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateProvince_StateProvinceID", x => x.StateProvinceID);
                    table.ForeignKey(
                        name: "FK_StateProvince_CountryRegion_CountryRegionCode",
                        column: x => x.CountryRegionCode,
                        principalSchema: "Person",
                        principalTable: "CountryRegion",
                        principalColumn: "CountryRegionCode");
                    table.ForeignKey(
                        name: "FK_StateProvince_SalesTerritory_TerritoryID",
                        column: x => x.TerritoryID,
                        principalSchema: "Sales",
                        principalTable: "SalesTerritory",
                        principalColumn: "TerritoryID");
                },
                comment: "State and province lookup table.");

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "Production",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false, comment: "Primary key for Product records.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Name of the product."),
                    ProductNumber = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false, comment: "Unique product identification number."),
                    MakeFlag = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "0 = Product is purchased, 1 = Product is manufactured in-house.")
                        .Annotation("Relational:DefaultConstraintName", "DF_Product_MakeFlag"),
                    FinishedGoodsFlag = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "0 = Product is not a salable item. 1 = Product is salable.")
                        .Annotation("Relational:DefaultConstraintName", "DF_Product_FinishedGoodsFlag"),
                    Color = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true, comment: "Product color."),
                    SafetyStockLevel = table.Column<short>(type: "smallint", nullable: false, comment: "Minimum inventory quantity. "),
                    ReorderPoint = table.Column<short>(type: "smallint", nullable: false, comment: "Inventory level that triggers a purchase order or work order. "),
                    StandardCost = table.Column<decimal>(type: "money", nullable: false, comment: "Standard cost of the product."),
                    ListPrice = table.Column<decimal>(type: "money", nullable: false, comment: "Selling price."),
                    Size = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true, comment: "Product size."),
                    SizeUnitMeasureCode = table.Column<string>(type: "nchar(3)", fixedLength: true, maxLength: 3, nullable: true, comment: "Unit of measure for Size column."),
                    WeightUnitMeasureCode = table.Column<string>(type: "nchar(3)", fixedLength: true, maxLength: 3, nullable: true, comment: "Unit of measure for Weight column."),
                    Weight = table.Column<decimal>(type: "decimal(8,2)", nullable: true, comment: "Product weight."),
                    DaysToManufacture = table.Column<int>(type: "int", nullable: false, comment: "Number of days required to manufacture the product."),
                    ProductLine = table.Column<string>(type: "nchar(2)", fixedLength: true, maxLength: 2, nullable: true, comment: "R = Road, M = Mountain, T = Touring, S = Standard"),
                    Class = table.Column<string>(type: "nchar(2)", fixedLength: true, maxLength: 2, nullable: true, comment: "H = High, M = Medium, L = Low"),
                    Style = table.Column<string>(type: "nchar(2)", fixedLength: true, maxLength: 2, nullable: true, comment: "W = Womens, M = Mens, U = Universal"),
                    ProductSubcategoryID = table.Column<int>(type: "int", nullable: true, comment: "Product is a member of this product subcategory. Foreign key to ProductSubCategory.ProductSubCategoryID. "),
                    ProductModelID = table.Column<int>(type: "int", nullable: true, comment: "Product is a member of this product model. Foreign key to ProductModel.ProductModelID."),
                    SellStartDate = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Date the product was available for sale."),
                    SellEndDate = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Date the product was no longer available for sale."),
                    DiscontinuedDate = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Date the product was discontinued."),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())", comment: "ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.")
                        .Annotation("Relational:DefaultConstraintName", "DF_Product_rowguid"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_Product_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_ProductID", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Product_ProductModel_ProductModelID",
                        column: x => x.ProductModelID,
                        principalSchema: "Production",
                        principalTable: "ProductModel",
                        principalColumn: "ProductModelID");
                    table.ForeignKey(
                        name: "FK_Product_ProductSubcategory_ProductSubcategoryID",
                        column: x => x.ProductSubcategoryID,
                        principalSchema: "Production",
                        principalTable: "ProductSubcategory",
                        principalColumn: "ProductSubcategoryID");
                    table.ForeignKey(
                        name: "FK_Product_UnitMeasure_SizeUnitMeasureCode",
                        column: x => x.SizeUnitMeasureCode,
                        principalSchema: "Production",
                        principalTable: "UnitMeasure",
                        principalColumn: "UnitMeasureCode");
                    table.ForeignKey(
                        name: "FK_Product_UnitMeasure_WeightUnitMeasureCode",
                        column: x => x.WeightUnitMeasureCode,
                        principalSchema: "Production",
                        principalTable: "UnitMeasure",
                        principalColumn: "UnitMeasureCode");
                },
                comment: "Products sold or used in the manfacturing of sold products.");

            migrationBuilder.CreateTable(
                name: "EmployeeDepartmentHistory",
                schema: "HumanResources",
                columns: table => new
                {
                    BusinessEntityID = table.Column<int>(type: "int", nullable: false, comment: "Employee identification number. Foreign key to Employee.BusinessEntityID."),
                    DepartmentID = table.Column<short>(type: "smallint", nullable: false, comment: "Department in which the employee worked including currently. Foreign key to Department.DepartmentID."),
                    ShiftID = table.Column<byte>(type: "tinyint", nullable: false, comment: "Identifies which 8-hour shift the employee works. Foreign key to Shift.Shift.ID."),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false, comment: "Date the employee started work in the department."),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true, comment: "Date the employee left the department. NULL = Current department."),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_EmployeeDepartmentHistory_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDepartmentHistory_BusinessEntityID_StartDate_DepartmentID", x => new { x.BusinessEntityID, x.StartDate, x.DepartmentID, x.ShiftID });
                    table.ForeignKey(
                        name: "FK_EmployeeDepartmentHistory_Department_DepartmentID",
                        column: x => x.DepartmentID,
                        principalSchema: "HumanResources",
                        principalTable: "Department",
                        principalColumn: "DepartmentID");
                    table.ForeignKey(
                        name: "FK_EmployeeDepartmentHistory_Employee_BusinessEntityID",
                        column: x => x.BusinessEntityID,
                        principalSchema: "HumanResources",
                        principalTable: "Employee",
                        principalColumn: "BusinessEntityID");
                    table.ForeignKey(
                        name: "FK_EmployeeDepartmentHistory_Shift_ShiftID",
                        column: x => x.ShiftID,
                        principalSchema: "HumanResources",
                        principalTable: "Shift",
                        principalColumn: "ShiftID");
                },
                comment: "Employee department transfers.");

            migrationBuilder.CreateTable(
                name: "EmployeePayHistory",
                schema: "HumanResources",
                columns: table => new
                {
                    BusinessEntityID = table.Column<int>(type: "int", nullable: false, comment: "Employee identification number. Foreign key to Employee.BusinessEntityID."),
                    RateChangeDate = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Date the change in pay is effective"),
                    Rate = table.Column<decimal>(type: "money", nullable: false, comment: "Salary hourly rate."),
                    PayFrequency = table.Column<byte>(type: "tinyint", nullable: false, comment: "1 = Salary received monthly, 2 = Salary received biweekly"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_EmployeePayHistory_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePayHistory_BusinessEntityID_RateChangeDate", x => new { x.BusinessEntityID, x.RateChangeDate });
                    table.ForeignKey(
                        name: "FK_EmployeePayHistory_Employee_BusinessEntityID",
                        column: x => x.BusinessEntityID,
                        principalSchema: "HumanResources",
                        principalTable: "Employee",
                        principalColumn: "BusinessEntityID");
                },
                comment: "Employee pay history.");

            migrationBuilder.CreateTable(
                name: "JobCandidate",
                schema: "HumanResources",
                columns: table => new
                {
                    JobCandidateID = table.Column<int>(type: "int", nullable: false, comment: "Primary key for JobCandidate records.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessEntityID = table.Column<int>(type: "int", nullable: true, comment: "Employee identification number if applicant was hired. Foreign key to Employee.BusinessEntityID."),
                    Resume = table.Column<string>(type: "xml", nullable: true, comment: "Résumé in XML format."),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_JobCandidate_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobCandidate_JobCandidateID", x => x.JobCandidateID);
                    table.ForeignKey(
                        name: "FK_JobCandidate_Employee_BusinessEntityID",
                        column: x => x.BusinessEntityID,
                        principalSchema: "HumanResources",
                        principalTable: "Employee",
                        principalColumn: "BusinessEntityID");
                },
                comment: "Résumés submitted to Human Resources by job applicants.");

            migrationBuilder.CreateTable(
                name: "PayrollDetail",
                schema: "HumanResources",
                columns: table => new
                {
                    PayrollDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PayrollId = table.Column<int>(type: "int", nullable: false),
                    BusinessEntityID = table.Column<int>(type: "int", nullable: false),
                    GrossSalary = table.Column<decimal>(type: "money", nullable: false),
                    SfsDeduction = table.Column<decimal>(type: "money", nullable: false),
                    AfpDeduction = table.Column<decimal>(type: "money", nullable: false),
                    IsrDeduction = table.Column<decimal>(type: "money", nullable: false),
                    OtherDeductions = table.Column<decimal>(type: "money", nullable: false),
                    NetSalary = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayrollDetail", x => x.PayrollDetailId);
                    table.ForeignKey(
                        name: "FK_PayrollDetail_Employee",
                        column: x => x.BusinessEntityID,
                        principalSchema: "HumanResources",
                        principalTable: "Employee",
                        principalColumn: "BusinessEntityID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayrollDetail_Payroll",
                        column: x => x.PayrollId,
                        principalSchema: "HumanResources",
                        principalTable: "Payroll",
                        principalColumn: "PayrollId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderHeader",
                schema: "Purchasing",
                columns: table => new
                {
                    PurchaseOrderID = table.Column<int>(type: "int", nullable: false, comment: "Primary key.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RevisionNumber = table.Column<byte>(type: "tinyint", nullable: false, comment: "Incremental number to track changes to the purchase order over time."),
                    Status = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)1, comment: "Order current status. 1 = Pending; 2 = Approved; 3 = Rejected; 4 = Complete")
                        .Annotation("Relational:DefaultConstraintName", "DF_PurchaseOrderHeader_Status"),
                    EmployeeID = table.Column<int>(type: "int", nullable: false, comment: "Employee who created the purchase order. Foreign key to Employee.BusinessEntityID."),
                    VendorID = table.Column<int>(type: "int", nullable: false, comment: "Vendor with whom the purchase order is placed. Foreign key to Vendor.BusinessEntityID."),
                    ShipMethodID = table.Column<int>(type: "int", nullable: false, comment: "Shipping method. Foreign key to ShipMethod.ShipMethodID."),
                    OrderDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Purchase order creation date.")
                        .Annotation("Relational:DefaultConstraintName", "DF_PurchaseOrderHeader_OrderDate"),
                    ShipDate = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Estimated shipment date from the vendor."),
                    SubTotal = table.Column<decimal>(type: "money", nullable: false, comment: "Purchase order subtotal. Computed as SUM(PurchaseOrderDetail.LineTotal)for the appropriate PurchaseOrderID."),
                    TaxAmt = table.Column<decimal>(type: "money", nullable: false, comment: "Tax amount."),
                    Freight = table.Column<decimal>(type: "money", nullable: false, comment: "Shipping cost."),
                    TotalDue = table.Column<decimal>(type: "money", nullable: false, computedColumnSql: "(isnull(([SubTotal]+[TaxAmt])+[Freight],(0)))", stored: true, comment: "Total due to vendor. Computed as Subtotal + TaxAmt + Freight."),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_PurchaseOrderHeader_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderHeader_PurchaseOrderID", x => x.PurchaseOrderID);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderHeader_Employee_EmployeeID",
                        column: x => x.EmployeeID,
                        principalSchema: "HumanResources",
                        principalTable: "Employee",
                        principalColumn: "BusinessEntityID");
                    table.ForeignKey(
                        name: "FK_PurchaseOrderHeader_ShipMethod_ShipMethodID",
                        column: x => x.ShipMethodID,
                        principalSchema: "Purchasing",
                        principalTable: "ShipMethod",
                        principalColumn: "ShipMethodID");
                    table.ForeignKey(
                        name: "FK_PurchaseOrderHeader_Vendor_VendorID",
                        column: x => x.VendorID,
                        principalSchema: "Purchasing",
                        principalTable: "Vendor",
                        principalColumn: "BusinessEntityID");
                },
                comment: "General purchase order information. See PurchaseOrderDetail.");

            migrationBuilder.CreateTable(
                name: "SalesPerson",
                schema: "Sales",
                columns: table => new
                {
                    BusinessEntityID = table.Column<int>(type: "int", nullable: false, comment: "Primary key for SalesPerson records. Foreign key to Employee.BusinessEntityID"),
                    TerritoryID = table.Column<int>(type: "int", nullable: true, comment: "Territory currently assigned to. Foreign key to SalesTerritory.SalesTerritoryID."),
                    SalesQuota = table.Column<decimal>(type: "money", nullable: true, comment: "Projected yearly sales."),
                    Bonus = table.Column<decimal>(type: "money", nullable: false, comment: "Bonus due if quota is met."),
                    CommissionPct = table.Column<decimal>(type: "smallmoney", nullable: false, comment: "Commision percent received per sale."),
                    SalesYTD = table.Column<decimal>(type: "money", nullable: false, comment: "Sales total year to date."),
                    SalesLastYear = table.Column<decimal>(type: "money", nullable: false, comment: "Sales total of previous year."),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())", comment: "ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.")
                        .Annotation("Relational:DefaultConstraintName", "DF_SalesPerson_rowguid"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_SalesPerson_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesPerson_BusinessEntityID", x => x.BusinessEntityID);
                    table.ForeignKey(
                        name: "FK_SalesPerson_Employee_BusinessEntityID",
                        column: x => x.BusinessEntityID,
                        principalSchema: "HumanResources",
                        principalTable: "Employee",
                        principalColumn: "BusinessEntityID");
                    table.ForeignKey(
                        name: "FK_SalesPerson_SalesTerritory_TerritoryID",
                        column: x => x.TerritoryID,
                        principalSchema: "Sales",
                        principalTable: "SalesTerritory",
                        principalColumn: "TerritoryID");
                },
                comment: "Sales representative current information.");

            migrationBuilder.CreateTable(
                name: "Address",
                schema: "Person",
                columns: table => new
                {
                    AddressID = table.Column<int>(type: "int", nullable: false, comment: "Primary key for Address records.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressLine1 = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false, comment: "First street address line."),
                    AddressLine2 = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true, comment: "Second street address line."),
                    City = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "Name of the city."),
                    StateProvinceID = table.Column<int>(type: "int", nullable: false, comment: "Unique identification number for the state or province. Foreign key to StateProvince table."),
                    PostalCode = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false, comment: "Postal code for the street address."),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())", comment: "ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.")
                        .Annotation("Relational:DefaultConstraintName", "DF_Address_rowguid"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_Address_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address_AddressID", x => x.AddressID);
                    table.ForeignKey(
                        name: "FK_Address_StateProvince_StateProvinceID",
                        column: x => x.StateProvinceID,
                        principalSchema: "Person",
                        principalTable: "StateProvince",
                        principalColumn: "StateProvinceID");
                },
                comment: "Street address information for customers, employees, and vendors.");

            migrationBuilder.CreateTable(
                name: "SalesTaxRate",
                schema: "Sales",
                columns: table => new
                {
                    SalesTaxRateID = table.Column<int>(type: "int", nullable: false, comment: "Primary key for SalesTaxRate records.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateProvinceID = table.Column<int>(type: "int", nullable: false, comment: "State, province, or country/region the sales tax applies to."),
                    TaxType = table.Column<byte>(type: "tinyint", nullable: false, comment: "1 = Tax applied to retail transactions, 2 = Tax applied to wholesale transactions, 3 = Tax applied to all sales (retail and wholesale) transactions."),
                    TaxRate = table.Column<decimal>(type: "smallmoney", nullable: false, comment: "Tax rate amount."),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Tax rate description."),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())", comment: "ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.")
                        .Annotation("Relational:DefaultConstraintName", "DF_SalesTaxRate_rowguid"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_SalesTaxRate_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesTaxRate_SalesTaxRateID", x => x.SalesTaxRateID);
                    table.ForeignKey(
                        name: "FK_SalesTaxRate_StateProvince_StateProvinceID",
                        column: x => x.StateProvinceID,
                        principalSchema: "Person",
                        principalTable: "StateProvince",
                        principalColumn: "StateProvinceID");
                },
                comment: "Tax rate lookup table.");

            migrationBuilder.CreateTable(
                name: "BillOfMaterials",
                schema: "Production",
                columns: table => new
                {
                    BillOfMaterialsID = table.Column<int>(type: "int", nullable: false, comment: "Primary key for BillOfMaterials records.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductAssemblyID = table.Column<int>(type: "int", nullable: true, comment: "Parent product identification number. Foreign key to Product.ProductID."),
                    ComponentID = table.Column<int>(type: "int", nullable: false, comment: "Component identification number. Foreign key to Product.ProductID."),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date the component started being used in the assembly item.")
                        .Annotation("Relational:DefaultConstraintName", "DF_BillOfMaterials_StartDate"),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Date the component stopped being used in the assembly item."),
                    UnitMeasureCode = table.Column<string>(type: "nchar(3)", fixedLength: true, maxLength: 3, nullable: false, comment: "Standard code identifying the unit of measure for the quantity."),
                    BOMLevel = table.Column<short>(type: "smallint", nullable: false, comment: "Indicates the depth the component is from its parent (AssemblyID)."),
                    PerAssemblyQty = table.Column<decimal>(type: "decimal(8,2)", nullable: false, defaultValue: 1.00m, comment: "Quantity of the component needed to create the assembly.")
                        .Annotation("Relational:DefaultConstraintName", "DF_BillOfMaterials_PerAssemblyQty"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_BillOfMaterials_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillOfMaterials_BillOfMaterialsID", x => x.BillOfMaterialsID)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_BillOfMaterials_Product_ComponentID",
                        column: x => x.ComponentID,
                        principalSchema: "Production",
                        principalTable: "Product",
                        principalColumn: "ProductID");
                    table.ForeignKey(
                        name: "FK_BillOfMaterials_Product_ProductAssemblyID",
                        column: x => x.ProductAssemblyID,
                        principalSchema: "Production",
                        principalTable: "Product",
                        principalColumn: "ProductID");
                    table.ForeignKey(
                        name: "FK_BillOfMaterials_UnitMeasure_UnitMeasureCode",
                        column: x => x.UnitMeasureCode,
                        principalSchema: "Production",
                        principalTable: "UnitMeasure",
                        principalColumn: "UnitMeasureCode");
                },
                comment: "Items required to make bicycles and bicycle subassemblies. It identifies the heirarchical relationship between a parent product and its components.");

            migrationBuilder.CreateTable(
                name: "ProductCostHistory",
                schema: "Production",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false, comment: "Product identification number. Foreign key to Product.ProductID"),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Product cost start date."),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Product cost end date."),
                    StandardCost = table.Column<decimal>(type: "money", nullable: false, comment: "Standard cost of the product."),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_ProductCostHistory_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCostHistory_ProductID_StartDate", x => new { x.ProductID, x.StartDate });
                    table.ForeignKey(
                        name: "FK_ProductCostHistory_Product_ProductID",
                        column: x => x.ProductID,
                        principalSchema: "Production",
                        principalTable: "Product",
                        principalColumn: "ProductID");
                },
                comment: "Changes in the cost of a product over time.");

            migrationBuilder.CreateTable(
                name: "ProductInventory",
                schema: "Production",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false, comment: "Product identification number. Foreign key to Product.ProductID."),
                    LocationID = table.Column<short>(type: "smallint", nullable: false, comment: "Inventory location identification number. Foreign key to Location.LocationID. "),
                    Shelf = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, comment: "Storage compartment within an inventory location."),
                    Bin = table.Column<byte>(type: "tinyint", nullable: false, comment: "Storage container on a shelf in an inventory location."),
                    Quantity = table.Column<short>(type: "smallint", nullable: false, comment: "Quantity of products in the inventory location."),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())", comment: "ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.")
                        .Annotation("Relational:DefaultConstraintName", "DF_ProductInventory_rowguid"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_ProductInventory_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInventory_ProductID_LocationID", x => new { x.ProductID, x.LocationID });
                    table.ForeignKey(
                        name: "FK_ProductInventory_Location_LocationID",
                        column: x => x.LocationID,
                        principalSchema: "Production",
                        principalTable: "Location",
                        principalColumn: "LocationID");
                    table.ForeignKey(
                        name: "FK_ProductInventory_Product_ProductID",
                        column: x => x.ProductID,
                        principalSchema: "Production",
                        principalTable: "Product",
                        principalColumn: "ProductID");
                },
                comment: "Product inventory information.");

            migrationBuilder.CreateTable(
                name: "ProductListPriceHistory",
                schema: "Production",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false, comment: "Product identification number. Foreign key to Product.ProductID"),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false, comment: "List price start date."),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true, comment: "List price end date"),
                    ListPrice = table.Column<decimal>(type: "money", nullable: false, comment: "Product list price."),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_ProductListPriceHistory_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductListPriceHistory_ProductID_StartDate", x => new { x.ProductID, x.StartDate });
                    table.ForeignKey(
                        name: "FK_ProductListPriceHistory_Product_ProductID",
                        column: x => x.ProductID,
                        principalSchema: "Production",
                        principalTable: "Product",
                        principalColumn: "ProductID");
                },
                comment: "Changes in the list price of a product over time.");

            migrationBuilder.CreateTable(
                name: "ProductProductPhoto",
                schema: "Production",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false, comment: "Product identification number. Foreign key to Product.ProductID."),
                    ProductPhotoID = table.Column<int>(type: "int", nullable: false, comment: "Product photo identification number. Foreign key to ProductPhoto.ProductPhotoID."),
                    Primary = table.Column<bool>(type: "bit", nullable: false, comment: "0 = Photo is not the principal image. 1 = Photo is the principal image."),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_ProductProductPhoto_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProductPhoto_ProductID_ProductPhotoID", x => new { x.ProductID, x.ProductPhotoID })
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_ProductProductPhoto_ProductPhoto_ProductPhotoID",
                        column: x => x.ProductPhotoID,
                        principalSchema: "Production",
                        principalTable: "ProductPhoto",
                        principalColumn: "ProductPhotoID");
                    table.ForeignKey(
                        name: "FK_ProductProductPhoto_Product_ProductID",
                        column: x => x.ProductID,
                        principalSchema: "Production",
                        principalTable: "Product",
                        principalColumn: "ProductID");
                },
                comment: "Cross-reference table mapping products and product photos.");

            migrationBuilder.CreateTable(
                name: "ProductReview",
                schema: "Production",
                columns: table => new
                {
                    ProductReviewID = table.Column<int>(type: "int", nullable: false, comment: "Primary key for ProductReview records.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: false, comment: "Product identification number. Foreign key to Product.ProductID."),
                    ReviewerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Name of the reviewer."),
                    ReviewDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date review was submitted.")
                        .Annotation("Relational:DefaultConstraintName", "DF_ProductReview_ReviewDate"),
                    EmailAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Reviewer's e-mail address."),
                    Rating = table.Column<int>(type: "int", nullable: false, comment: "Product rating given by the reviewer. Scale is 1 to 5 with 5 as the highest rating."),
                    Comments = table.Column<string>(type: "nvarchar(3850)", maxLength: 3850, nullable: true, comment: "Reviewer's comments"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_ProductReview_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductReview_ProductReviewID", x => x.ProductReviewID);
                    table.ForeignKey(
                        name: "FK_ProductReview_Product_ProductID",
                        column: x => x.ProductID,
                        principalSchema: "Production",
                        principalTable: "Product",
                        principalColumn: "ProductID");
                },
                comment: "Customer reviews of products they have purchased.");

            migrationBuilder.CreateTable(
                name: "ProductVendor",
                schema: "Purchasing",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false, comment: "Primary key. Foreign key to Product.ProductID."),
                    BusinessEntityID = table.Column<int>(type: "int", nullable: false, comment: "Primary key. Foreign key to Vendor.BusinessEntityID."),
                    AverageLeadTime = table.Column<int>(type: "int", nullable: false, comment: "The average span of time (in days) between placing an order with the vendor and receiving the purchased product."),
                    StandardPrice = table.Column<decimal>(type: "money", nullable: false, comment: "The vendor's usual selling price."),
                    LastReceiptCost = table.Column<decimal>(type: "money", nullable: true, comment: "The selling price when last purchased."),
                    LastReceiptDate = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Date the product was last received by the vendor."),
                    MinOrderQty = table.Column<int>(type: "int", nullable: false, comment: "The maximum quantity that should be ordered."),
                    MaxOrderQty = table.Column<int>(type: "int", nullable: false, comment: "The minimum quantity that should be ordered."),
                    OnOrderQty = table.Column<int>(type: "int", nullable: true, comment: "The quantity currently on order."),
                    UnitMeasureCode = table.Column<string>(type: "nchar(3)", fixedLength: true, maxLength: 3, nullable: false, comment: "The product's unit of measure."),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_ProductVendor_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVendor_ProductID_BusinessEntityID", x => new { x.ProductID, x.BusinessEntityID });
                    table.ForeignKey(
                        name: "FK_ProductVendor_Product_ProductID",
                        column: x => x.ProductID,
                        principalSchema: "Production",
                        principalTable: "Product",
                        principalColumn: "ProductID");
                    table.ForeignKey(
                        name: "FK_ProductVendor_UnitMeasure_UnitMeasureCode",
                        column: x => x.UnitMeasureCode,
                        principalSchema: "Production",
                        principalTable: "UnitMeasure",
                        principalColumn: "UnitMeasureCode");
                    table.ForeignKey(
                        name: "FK_ProductVendor_Vendor_BusinessEntityID",
                        column: x => x.BusinessEntityID,
                        principalSchema: "Purchasing",
                        principalTable: "Vendor",
                        principalColumn: "BusinessEntityID");
                },
                comment: "Cross-reference table mapping vendors with the products they supply.");

            migrationBuilder.CreateTable(
                name: "ShoppingCartItem",
                schema: "Sales",
                columns: table => new
                {
                    ShoppingCartItemID = table.Column<int>(type: "int", nullable: false, comment: "Primary key for ShoppingCartItem records.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShoppingCartID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Shopping cart identification number."),
                    Quantity = table.Column<int>(type: "int", nullable: false, defaultValue: 1, comment: "Product quantity ordered.")
                        .Annotation("Relational:DefaultConstraintName", "DF_ShoppingCartItem_Quantity"),
                    ProductID = table.Column<int>(type: "int", nullable: false, comment: "Product ordered. Foreign key to Product.ProductID."),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date the time the record was created.")
                        .Annotation("Relational:DefaultConstraintName", "DF_ShoppingCartItem_DateCreated"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_ShoppingCartItem_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartItem_ShoppingCartItemID", x => x.ShoppingCartItemID);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItem_Product_ProductID",
                        column: x => x.ProductID,
                        principalSchema: "Production",
                        principalTable: "Product",
                        principalColumn: "ProductID");
                },
                comment: "Contains online customer orders until the order is submitted or cancelled.");

            migrationBuilder.CreateTable(
                name: "SpecialOfferProduct",
                schema: "Sales",
                columns: table => new
                {
                    SpecialOfferID = table.Column<int>(type: "int", nullable: false, comment: "Primary key for SpecialOfferProduct records."),
                    ProductID = table.Column<int>(type: "int", nullable: false, comment: "Product identification number. Foreign key to Product.ProductID."),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())", comment: "ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.")
                        .Annotation("Relational:DefaultConstraintName", "DF_SpecialOfferProduct_rowguid"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_SpecialOfferProduct_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialOfferProduct_SpecialOfferID_ProductID", x => new { x.SpecialOfferID, x.ProductID });
                    table.ForeignKey(
                        name: "FK_SpecialOfferProduct_Product_ProductID",
                        column: x => x.ProductID,
                        principalSchema: "Production",
                        principalTable: "Product",
                        principalColumn: "ProductID");
                    table.ForeignKey(
                        name: "FK_SpecialOfferProduct_SpecialOffer_SpecialOfferID",
                        column: x => x.SpecialOfferID,
                        principalSchema: "Sales",
                        principalTable: "SpecialOffer",
                        principalColumn: "SpecialOfferID");
                },
                comment: "Cross-reference table mapping products to special offer discounts.");

            migrationBuilder.CreateTable(
                name: "TransactionHistory",
                schema: "Production",
                columns: table => new
                {
                    TransactionID = table.Column<int>(type: "int", nullable: false, comment: "Primary key for TransactionHistory records.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: false, comment: "Product identification number. Foreign key to Product.ProductID."),
                    ReferenceOrderID = table.Column<int>(type: "int", nullable: false, comment: "Purchase order, sales order, or work order identification number."),
                    ReferenceOrderLineID = table.Column<int>(type: "int", nullable: false, comment: "Line number associated with the purchase order, sales order, or work order."),
                    TransactionDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time of the transaction.")
                        .Annotation("Relational:DefaultConstraintName", "DF_TransactionHistory_TransactionDate"),
                    TransactionType = table.Column<string>(type: "nchar(1)", fixedLength: true, maxLength: 1, nullable: false, comment: "W = WorkOrder, S = SalesOrder, P = PurchaseOrder"),
                    Quantity = table.Column<int>(type: "int", nullable: false, comment: "Product quantity."),
                    ActualCost = table.Column<decimal>(type: "money", nullable: false, comment: "Product cost."),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_TransactionHistory_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionHistory_TransactionID", x => x.TransactionID);
                    table.ForeignKey(
                        name: "FK_TransactionHistory_Product_ProductID",
                        column: x => x.ProductID,
                        principalSchema: "Production",
                        principalTable: "Product",
                        principalColumn: "ProductID");
                },
                comment: "Record of each purchase order, sales order, or work order transaction year to date.");

            migrationBuilder.CreateTable(
                name: "WorkOrder",
                schema: "Production",
                columns: table => new
                {
                    WorkOrderID = table.Column<int>(type: "int", nullable: false, comment: "Primary key for WorkOrder records.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: false, comment: "Product identification number. Foreign key to Product.ProductID."),
                    OrderQty = table.Column<int>(type: "int", nullable: false, comment: "Product quantity to build."),
                    StockedQty = table.Column<int>(type: "int", nullable: false, computedColumnSql: "(isnull([OrderQty]-[ScrappedQty],(0)))", stored: false, comment: "Quantity built and put in inventory."),
                    ScrappedQty = table.Column<short>(type: "smallint", nullable: false, comment: "Quantity that failed inspection."),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Work order start date."),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Work order end date."),
                    DueDate = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Work order due date."),
                    ScrapReasonID = table.Column<short>(type: "smallint", nullable: true, comment: "Reason for inspection failure."),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_WorkOrder_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrder_WorkOrderID", x => x.WorkOrderID);
                    table.ForeignKey(
                        name: "FK_WorkOrder_Product_ProductID",
                        column: x => x.ProductID,
                        principalSchema: "Production",
                        principalTable: "Product",
                        principalColumn: "ProductID");
                    table.ForeignKey(
                        name: "FK_WorkOrder_ScrapReason_ScrapReasonID",
                        column: x => x.ScrapReasonID,
                        principalSchema: "Production",
                        principalTable: "ScrapReason",
                        principalColumn: "ScrapReasonID");
                },
                comment: "Manufacturing work orders.");

            migrationBuilder.CreateTable(
                name: "PurchaseOrderDetail",
                schema: "Purchasing",
                columns: table => new
                {
                    PurchaseOrderID = table.Column<int>(type: "int", nullable: false, comment: "Primary key. Foreign key to PurchaseOrderHeader.PurchaseOrderID."),
                    PurchaseOrderDetailID = table.Column<int>(type: "int", nullable: false, comment: "Primary key. One line number per purchased product.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DueDate = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Date the product is expected to be received."),
                    OrderQty = table.Column<short>(type: "smallint", nullable: false, comment: "Quantity ordered."),
                    ProductID = table.Column<int>(type: "int", nullable: false, comment: "Product identification number. Foreign key to Product.ProductID."),
                    UnitPrice = table.Column<decimal>(type: "money", nullable: false, comment: "Vendor's selling price of a single product."),
                    LineTotal = table.Column<decimal>(type: "money", nullable: false, computedColumnSql: "(isnull([OrderQty]*[UnitPrice],(0.00)))", stored: false, comment: "Per product subtotal. Computed as OrderQty * UnitPrice."),
                    ReceivedQty = table.Column<decimal>(type: "decimal(8,2)", nullable: false, comment: "Quantity actually received from the vendor."),
                    RejectedQty = table.Column<decimal>(type: "decimal(8,2)", nullable: false, comment: "Quantity rejected during inspection."),
                    StockedQty = table.Column<decimal>(type: "decimal(9,2)", nullable: false, computedColumnSql: "(isnull([ReceivedQty]-[RejectedQty],(0.00)))", stored: false, comment: "Quantity accepted into inventory. Computed as ReceivedQty - RejectedQty."),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_PurchaseOrderDetail_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderDetail_PurchaseOrderID_PurchaseOrderDetailID", x => new { x.PurchaseOrderID, x.PurchaseOrderDetailID });
                    table.ForeignKey(
                        name: "FK_PurchaseOrderDetail_Product_ProductID",
                        column: x => x.ProductID,
                        principalSchema: "Production",
                        principalTable: "Product",
                        principalColumn: "ProductID");
                    table.ForeignKey(
                        name: "FK_PurchaseOrderDetail_PurchaseOrderHeader_PurchaseOrderID",
                        column: x => x.PurchaseOrderID,
                        principalSchema: "Purchasing",
                        principalTable: "PurchaseOrderHeader",
                        principalColumn: "PurchaseOrderID");
                },
                comment: "Individual products associated with a specific purchase order. See PurchaseOrderHeader.");

            migrationBuilder.CreateTable(
                name: "SalesPersonQuotaHistory",
                schema: "Sales",
                columns: table => new
                {
                    BusinessEntityID = table.Column<int>(type: "int", nullable: false, comment: "Sales person identification number. Foreign key to SalesPerson.BusinessEntityID."),
                    QuotaDate = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Sales quota date."),
                    SalesQuota = table.Column<decimal>(type: "money", nullable: false, comment: "Sales quota amount."),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())", comment: "ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.")
                        .Annotation("Relational:DefaultConstraintName", "DF_SalesPersonQuotaHistory_rowguid"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_SalesPersonQuotaHistory_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesPersonQuotaHistory_BusinessEntityID_QuotaDate", x => new { x.BusinessEntityID, x.QuotaDate });
                    table.ForeignKey(
                        name: "FK_SalesPersonQuotaHistory_SalesPerson_BusinessEntityID",
                        column: x => x.BusinessEntityID,
                        principalSchema: "Sales",
                        principalTable: "SalesPerson",
                        principalColumn: "BusinessEntityID");
                },
                comment: "Sales performance tracking.");

            migrationBuilder.CreateTable(
                name: "SalesTerritoryHistory",
                schema: "Sales",
                columns: table => new
                {
                    BusinessEntityID = table.Column<int>(type: "int", nullable: false, comment: "Primary key. The sales rep.  Foreign key to SalesPerson.BusinessEntityID."),
                    TerritoryID = table.Column<int>(type: "int", nullable: false, comment: "Primary key. Territory identification number. Foreign key to SalesTerritory.SalesTerritoryID."),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Primary key. Date the sales representive started work in the territory."),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Date the sales representative left work in the territory."),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())", comment: "ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.")
                        .Annotation("Relational:DefaultConstraintName", "DF_SalesTerritoryHistory_rowguid"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_SalesTerritoryHistory_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesTerritoryHistory_BusinessEntityID_StartDate_TerritoryID", x => new { x.BusinessEntityID, x.StartDate, x.TerritoryID });
                    table.ForeignKey(
                        name: "FK_SalesTerritoryHistory_SalesPerson_BusinessEntityID",
                        column: x => x.BusinessEntityID,
                        principalSchema: "Sales",
                        principalTable: "SalesPerson",
                        principalColumn: "BusinessEntityID");
                    table.ForeignKey(
                        name: "FK_SalesTerritoryHistory_SalesTerritory_TerritoryID",
                        column: x => x.TerritoryID,
                        principalSchema: "Sales",
                        principalTable: "SalesTerritory",
                        principalColumn: "TerritoryID");
                },
                comment: "Sales representative transfers to other sales territories.");

            migrationBuilder.CreateTable(
                name: "Store",
                schema: "Sales",
                columns: table => new
                {
                    BusinessEntityID = table.Column<int>(type: "int", nullable: false, comment: "Primary key. Foreign key to Customer.BusinessEntityID."),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Name of the store."),
                    SalesPersonID = table.Column<int>(type: "int", nullable: true, comment: "ID of the sales person assigned to the customer. Foreign key to SalesPerson.BusinessEntityID."),
                    Demographics = table.Column<string>(type: "xml", nullable: true, comment: "Demographic informationg about the store such as the number of employees, annual sales and store type."),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())", comment: "ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.")
                        .Annotation("Relational:DefaultConstraintName", "DF_Store_rowguid"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_Store_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store_BusinessEntityID", x => x.BusinessEntityID);
                    table.ForeignKey(
                        name: "FK_Store_BusinessEntity_BusinessEntityID",
                        column: x => x.BusinessEntityID,
                        principalSchema: "Person",
                        principalTable: "BusinessEntity",
                        principalColumn: "BusinessEntityID");
                    table.ForeignKey(
                        name: "FK_Store_SalesPerson_SalesPersonID",
                        column: x => x.SalesPersonID,
                        principalSchema: "Sales",
                        principalTable: "SalesPerson",
                        principalColumn: "BusinessEntityID");
                },
                comment: "Customers (resellers) of Adventure Works products.");

            migrationBuilder.CreateTable(
                name: "BusinessEntityAddress",
                schema: "Person",
                columns: table => new
                {
                    BusinessEntityID = table.Column<int>(type: "int", nullable: false, comment: "Primary key. Foreign key to BusinessEntity.BusinessEntityID."),
                    AddressID = table.Column<int>(type: "int", nullable: false, comment: "Primary key. Foreign key to Address.AddressID."),
                    AddressTypeID = table.Column<int>(type: "int", nullable: false, comment: "Primary key. Foreign key to AddressType.AddressTypeID."),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())", comment: "ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.")
                        .Annotation("Relational:DefaultConstraintName", "DF_BusinessEntityAddress_rowguid"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_BusinessEntityAddress_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessEntityAddress_BusinessEntityID_AddressID_AddressTypeID", x => new { x.BusinessEntityID, x.AddressID, x.AddressTypeID });
                    table.ForeignKey(
                        name: "FK_BusinessEntityAddress_AddressType_AddressTypeID",
                        column: x => x.AddressTypeID,
                        principalSchema: "Person",
                        principalTable: "AddressType",
                        principalColumn: "AddressTypeID");
                    table.ForeignKey(
                        name: "FK_BusinessEntityAddress_Address_AddressID",
                        column: x => x.AddressID,
                        principalSchema: "Person",
                        principalTable: "Address",
                        principalColumn: "AddressID");
                    table.ForeignKey(
                        name: "FK_BusinessEntityAddress_BusinessEntity_BusinessEntityID",
                        column: x => x.BusinessEntityID,
                        principalSchema: "Person",
                        principalTable: "BusinessEntity",
                        principalColumn: "BusinessEntityID");
                },
                comment: "Cross-reference table mapping customers, vendors, and employees to their addresses.");

            migrationBuilder.CreateTable(
                name: "WorkOrderRouting",
                schema: "Production",
                columns: table => new
                {
                    WorkOrderID = table.Column<int>(type: "int", nullable: false, comment: "Primary key. Foreign key to WorkOrder.WorkOrderID."),
                    ProductID = table.Column<int>(type: "int", nullable: false, comment: "Primary key. Foreign key to Product.ProductID."),
                    OperationSequence = table.Column<short>(type: "smallint", nullable: false, comment: "Primary key. Indicates the manufacturing process sequence."),
                    LocationID = table.Column<short>(type: "smallint", nullable: false, comment: "Manufacturing location where the part is processed. Foreign key to Location.LocationID."),
                    ScheduledStartDate = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Planned manufacturing start date."),
                    ScheduledEndDate = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Planned manufacturing end date."),
                    ActualStartDate = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Actual start date."),
                    ActualEndDate = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Actual end date."),
                    ActualResourceHrs = table.Column<decimal>(type: "decimal(9,4)", nullable: true, comment: "Number of manufacturing hours used."),
                    PlannedCost = table.Column<decimal>(type: "money", nullable: false, comment: "Estimated manufacturing cost."),
                    ActualCost = table.Column<decimal>(type: "money", nullable: true, comment: "Actual manufacturing cost."),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_WorkOrderRouting_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderRouting_WorkOrderID_ProductID_OperationSequence", x => new { x.WorkOrderID, x.ProductID, x.OperationSequence });
                    table.ForeignKey(
                        name: "FK_WorkOrderRouting_Location_LocationID",
                        column: x => x.LocationID,
                        principalSchema: "Production",
                        principalTable: "Location",
                        principalColumn: "LocationID");
                    table.ForeignKey(
                        name: "FK_WorkOrderRouting_WorkOrder_WorkOrderID",
                        column: x => x.WorkOrderID,
                        principalSchema: "Production",
                        principalTable: "WorkOrder",
                        principalColumn: "WorkOrderID");
                },
                comment: "Work order details.");

            migrationBuilder.CreateTable(
                name: "Customer",
                schema: "Sales",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false, comment: "Primary key.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonID = table.Column<int>(type: "int", nullable: true, comment: "Foreign key to Person.BusinessEntityID"),
                    StoreID = table.Column<int>(type: "int", nullable: true, comment: "Foreign key to Store.BusinessEntityID"),
                    TerritoryID = table.Column<int>(type: "int", nullable: true, comment: "ID of the territory in which the customer is located. Foreign key to SalesTerritory.SalesTerritoryID."),
                    AccountNumber = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false, computedColumnSql: "(isnull('AW'+[dbo].[ufnLeadingZeros]([CustomerID]),''))", stored: false, comment: "Unique number identifying the customer assigned by the accounting system."),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())", comment: "ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.")
                        .Annotation("Relational:DefaultConstraintName", "DF_Customer_rowguid"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_Customer_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer_CustomerID", x => x.CustomerID);
                    table.ForeignKey(
                        name: "FK_Customer_Person_PersonID",
                        column: x => x.PersonID,
                        principalSchema: "Person",
                        principalTable: "Person",
                        principalColumn: "BusinessEntityID");
                    table.ForeignKey(
                        name: "FK_Customer_SalesTerritory_TerritoryID",
                        column: x => x.TerritoryID,
                        principalSchema: "Sales",
                        principalTable: "SalesTerritory",
                        principalColumn: "TerritoryID");
                    table.ForeignKey(
                        name: "FK_Customer_Store_StoreID",
                        column: x => x.StoreID,
                        principalSchema: "Sales",
                        principalTable: "Store",
                        principalColumn: "BusinessEntityID");
                },
                comment: "Current customer information. Also see the Person and Store tables.");

            migrationBuilder.CreateTable(
                name: "SalesOrderHeader",
                schema: "Sales",
                columns: table => new
                {
                    SalesOrderID = table.Column<int>(type: "int", nullable: false, comment: "Primary key.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RevisionNumber = table.Column<byte>(type: "tinyint", nullable: false, comment: "Incremental number to track changes to the sales order over time."),
                    OrderDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Dates the sales order was created.")
                        .Annotation("Relational:DefaultConstraintName", "DF_SalesOrderHeader_OrderDate"),
                    DueDate = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Date the order is due to the customer."),
                    ShipDate = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Date the order was shipped to the customer."),
                    Status = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)1, comment: "Order current status. 1 = In process; 2 = Approved; 3 = Backordered; 4 = Rejected; 5 = Shipped; 6 = Cancelled")
                        .Annotation("Relational:DefaultConstraintName", "DF_SalesOrderHeader_Status"),
                    OnlineOrderFlag = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "0 = Order placed by sales person. 1 = Order placed online by customer.")
                        .Annotation("Relational:DefaultConstraintName", "DF_SalesOrderHeader_OnlineOrderFlag"),
                    SalesOrderNumber = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false, computedColumnSql: "(isnull(N'SO'+CONVERT([nvarchar](23),[SalesOrderID]),N'*** ERROR ***'))", stored: false, comment: "Unique sales order identification number."),
                    PurchaseOrderNumber = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true, comment: "Customer purchase order number reference. "),
                    AccountNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true, comment: "Financial accounting number reference."),
                    CustomerID = table.Column<int>(type: "int", nullable: false, comment: "Customer identification number. Foreign key to Customer.BusinessEntityID."),
                    SalesPersonID = table.Column<int>(type: "int", nullable: true, comment: "Sales person who created the sales order. Foreign key to SalesPerson.BusinessEntityID."),
                    TerritoryID = table.Column<int>(type: "int", nullable: false, comment: "Territory in which the sale was made. Foreign key to SalesTerritory.SalesTerritoryID."),
                    BillToAddressID = table.Column<int>(type: "int", nullable: false, comment: "Customer billing address. Foreign key to Address.AddressID."),
                    ShipToAddressID = table.Column<int>(type: "int", nullable: false, comment: "Customer shipping address. Foreign key to Address.AddressID."),
                    ShipMethodID = table.Column<int>(type: "int", nullable: false, comment: "Shipping method. Foreign key to ShipMethod.ShipMethodID."),
                    CreditCardID = table.Column<int>(type: "int", nullable: false, comment: "Credit card identification number. Foreign key to CreditCard.CreditCardID."),
                    CreditCardApprovalCode = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true, comment: "Approval code provided by the credit card company."),
                    CurrencyRateID = table.Column<int>(type: "int", nullable: true, comment: "Currency exchange rate used. Foreign key to CurrencyRate.CurrencyRateID."),
                    SubTotal = table.Column<decimal>(type: "money", nullable: false, comment: "Sales subtotal. Computed as SUM(SalesOrderDetail.LineTotal)for the appropriate SalesOrderID."),
                    TaxAmt = table.Column<decimal>(type: "money", nullable: false, comment: "Tax amount."),
                    Freight = table.Column<decimal>(type: "money", nullable: false, comment: "Shipping cost."),
                    TotalDue = table.Column<decimal>(type: "money", nullable: false, computedColumnSql: "(isnull(([SubTotal]+[TaxAmt])+[Freight],(0)))", stored: false, comment: "Total due from customer. Computed as Subtotal + TaxAmt + Freight."),
                    Comment = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true, comment: "Sales representative comments."),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())", comment: "ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.")
                        .Annotation("Relational:DefaultConstraintName", "DF_SalesOrderHeader_rowguid"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_SalesOrderHeader_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrderHeader_SalesOrderID", x => x.SalesOrderID);
                    table.ForeignKey(
                        name: "FK_SalesOrderHeader_Address_BillToAddressID",
                        column: x => x.BillToAddressID,
                        principalSchema: "Person",
                        principalTable: "Address",
                        principalColumn: "AddressID");
                    table.ForeignKey(
                        name: "FK_SalesOrderHeader_Address_ShipToAddressID",
                        column: x => x.ShipToAddressID,
                        principalSchema: "Person",
                        principalTable: "Address",
                        principalColumn: "AddressID");
                    table.ForeignKey(
                        name: "FK_SalesOrderHeader_CreditCard_CreditCardID",
                        column: x => x.CreditCardID,
                        principalSchema: "Sales",
                        principalTable: "CreditCard",
                        principalColumn: "CreditCardID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesOrderHeader_CurrencyRate_CurrencyRateID",
                        column: x => x.CurrencyRateID,
                        principalSchema: "Sales",
                        principalTable: "CurrencyRate",
                        principalColumn: "CurrencyRateID");
                    table.ForeignKey(
                        name: "FK_SalesOrderHeader_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalSchema: "Sales",
                        principalTable: "Customer",
                        principalColumn: "CustomerID");
                    table.ForeignKey(
                        name: "FK_SalesOrderHeader_SalesPerson_SalesPersonID",
                        column: x => x.SalesPersonID,
                        principalSchema: "Sales",
                        principalTable: "SalesPerson",
                        principalColumn: "BusinessEntityID");
                    table.ForeignKey(
                        name: "FK_SalesOrderHeader_SalesTerritory_TerritoryID",
                        column: x => x.TerritoryID,
                        principalSchema: "Sales",
                        principalTable: "SalesTerritory",
                        principalColumn: "TerritoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesOrderHeader_ShipMethod_ShipMethodID",
                        column: x => x.ShipMethodID,
                        principalSchema: "Purchasing",
                        principalTable: "ShipMethod",
                        principalColumn: "ShipMethodID");
                },
                comment: "General sales order information.");

            migrationBuilder.CreateTable(
                name: "SalesOrderDetail",
                schema: "Sales",
                columns: table => new
                {
                    SalesOrderID = table.Column<int>(type: "int", nullable: false, comment: "Primary key. Foreign key to SalesOrderHeader.SalesOrderID."),
                    SalesOrderDetailID = table.Column<int>(type: "int", nullable: false, comment: "Primary key. One incremental unique number per product sold.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarrierTrackingNumber = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true, comment: "Shipment tracking number supplied by the shipper."),
                    OrderQty = table.Column<short>(type: "smallint", nullable: false, comment: "Quantity ordered per product."),
                    ProductID = table.Column<int>(type: "int", nullable: false, comment: "Product sold to customer. Foreign key to Product.ProductID."),
                    SpecialOfferID = table.Column<int>(type: "int", nullable: false, comment: "Promotional code. Foreign key to SpecialOffer.SpecialOfferID."),
                    UnitPrice = table.Column<decimal>(type: "money", nullable: false, comment: "Selling price of a single product."),
                    UnitPriceDiscount = table.Column<decimal>(type: "money", nullable: false, comment: "Discount amount."),
                    LineTotal = table.Column<decimal>(type: "numeric(38,6)", nullable: false, computedColumnSql: "(isnull(([UnitPrice]*((1.0)-[UnitPriceDiscount]))*[OrderQty],(0.0)))", stored: false, comment: "Per product subtotal. Computed as UnitPrice * (1 - UnitPriceDiscount) * OrderQty."),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())", comment: "ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.")
                        .Annotation("Relational:DefaultConstraintName", "DF_SalesOrderDetail_rowguid"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_SalesOrderDetail_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrderDetail_SalesOrderID_SalesOrderDetailID", x => new { x.SalesOrderID, x.SalesOrderDetailID });
                    table.ForeignKey(
                        name: "FK_SalesOrderDetail_SalesOrderHeader_SalesOrderID",
                        column: x => x.SalesOrderID,
                        principalSchema: "Sales",
                        principalTable: "SalesOrderHeader",
                        principalColumn: "SalesOrderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesOrderDetail_SpecialOfferProduct_SpecialOfferIDProductID",
                        columns: x => new { x.SpecialOfferID, x.ProductID },
                        principalSchema: "Sales",
                        principalTable: "SpecialOfferProduct",
                        principalColumns: new[] { "SpecialOfferID", "ProductID" });
                },
                comment: "Individual products associated with a specific sales order. See SalesOrderHeader.");

            migrationBuilder.CreateTable(
                name: "SalesOrderHeaderSalesReason",
                schema: "Sales",
                columns: table => new
                {
                    SalesOrderID = table.Column<int>(type: "int", nullable: false, comment: "Primary key. Foreign key to SalesOrderHeader.SalesOrderID."),
                    SalesReasonID = table.Column<int>(type: "int", nullable: false, comment: "Primary key. Foreign key to SalesReason.SalesReasonID."),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date and time the record was last updated.")
                        .Annotation("Relational:DefaultConstraintName", "DF_SalesOrderHeaderSalesReason_ModifiedDate")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrderHeaderSalesReason_SalesOrderID_SalesReasonID", x => new { x.SalesOrderID, x.SalesReasonID });
                    table.ForeignKey(
                        name: "FK_SalesOrderHeaderSalesReason_SalesOrderHeader_SalesOrderID",
                        column: x => x.SalesOrderID,
                        principalSchema: "Sales",
                        principalTable: "SalesOrderHeader",
                        principalColumn: "SalesOrderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesOrderHeaderSalesReason_SalesReason_SalesReasonID",
                        column: x => x.SalesReasonID,
                        principalSchema: "Sales",
                        principalTable: "SalesReason",
                        principalColumn: "SalesReasonID");
                },
                comment: "Cross-reference table mapping sales orders to sales reason codes.");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4af5ac9c-cd9e-4b4b-9dd4-91519347ed9c", "d6b7b2de-92af-425b-8664-df82089ba9c2", "ProductionManager", "PRODUCTIONMANAGER" },
                    { "601f85f6-e309-46a3-84ea-2ac355f2848b", "a718302f-5374-42f1-aa8f-d232cf628213", "HRManager", "HRMANAGER" },
                    { "846957c0-ad3b-4063-b29b-76d81478a6d9", "e98e4d3c-353d-4cba-a185-115f5907edba", "PurchasingManager", "PURCHASINGMANAGER" },
                    { "a18be9c0-aa65-4af8-bd17-00bd9344e575", "7a151b4a-81f1-419b-ab28-7fb0661266ec", "Admin", "ADMIN" },
                    { "b29ce9c0-bb65-4af8-bd17-00bd9344e576", "f2a281ab-2a44-48db-a2d0-06fbf3099908", "Vendor", "VENDOR" },
                    { "be4d0ea3-4436-4914-a7ac-ae204885beb1", "8b23c218-c2b6-455b-82a1-b4f0b2f15591", "SalesManager", "SALESMANAGER" },
                    { "c30de9c0-cc65-4af8-bd17-00bd9344e577", "c5b2ea1d-84bf-42cd-9430-b5cc8579d460", "Customer", "CUSTOMER" }
                });

            migrationBuilder.InsertData(
                schema: "Production",
                table: "UnitMeasure",
                columns: new[] { "UnitMeasureCode", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { "BOX", new DateTime(2026, 4, 11, 12, 0, 0, 0, DateTimeKind.Unspecified), "Box" },
                    { "CM", new DateTime(2026, 4, 11, 12, 0, 0, 0, DateTimeKind.Unspecified), "Centimeter" },
                    { "DZ", new DateTime(2026, 4, 11, 12, 0, 0, 0, DateTimeKind.Unspecified), "Dozen" },
                    { "FT", new DateTime(2026, 4, 11, 12, 0, 0, 0, DateTimeKind.Unspecified), "Foot" },
                    { "G", new DateTime(2026, 4, 11, 12, 0, 0, 0, DateTimeKind.Unspecified), "Gram" },
                    { "IN", new DateTime(2026, 4, 11, 12, 0, 0, 0, DateTimeKind.Unspecified), "Inch" },
                    { "KG", new DateTime(2026, 4, 11, 12, 0, 0, 0, DateTimeKind.Unspecified), "Kilogram" },
                    { "L", new DateTime(2026, 4, 11, 12, 0, 0, 0, DateTimeKind.Unspecified), "Liter" },
                    { "M", new DateTime(2026, 4, 11, 12, 0, 0, 0, DateTimeKind.Unspecified), "Meter" },
                    { "MG", new DateTime(2026, 4, 11, 12, 0, 0, 0, DateTimeKind.Unspecified), "Milligram" },
                    { "PK", new DateTime(2026, 4, 11, 12, 0, 0, 0, DateTimeKind.Unspecified), "Pack" },
                    { "YD", new DateTime(2026, 4, 11, 12, 0, 0, 0, DateTimeKind.Unspecified), "Yard" }
                });

            migrationBuilder.CreateIndex(
                name: "AK_Address_rowguid",
                schema: "Person",
                table: "Address",
                column: "rowguid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Address_AddressLine1_AddressLine2_City_StateProvinceID_PostalCode",
                schema: "Person",
                table: "Address",
                columns: new[] { "AddressLine1", "AddressLine2", "City", "StateProvinceID", "PostalCode" },
                unique: true,
                filter: "[AddressLine2] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Address_StateProvinceID",
                schema: "Person",
                table: "Address",
                column: "StateProvinceID");

            migrationBuilder.CreateIndex(
                name: "AK_AddressType_Name",
                schema: "Person",
                table: "AddressType",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AK_AddressType_rowguid",
                schema: "Person",
                table: "AddressType",
                column: "rowguid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "([NormalizedName] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserPasskeys_UserId",
                table: "AspNetUserPasskeys",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "([NormalizedUserName] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "AK_BillOfMaterials_ProductAssemblyID_ComponentID_StartDate",
                schema: "Production",
                table: "BillOfMaterials",
                columns: new[] { "ProductAssemblyID", "ComponentID", "StartDate" },
                unique: true)
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_BillOfMaterials_ComponentID",
                schema: "Production",
                table: "BillOfMaterials",
                column: "ComponentID");

            migrationBuilder.CreateIndex(
                name: "IX_BillOfMaterials_UnitMeasureCode",
                schema: "Production",
                table: "BillOfMaterials",
                column: "UnitMeasureCode");

            migrationBuilder.CreateIndex(
                name: "AK_BusinessEntity_rowguid",
                schema: "Person",
                table: "BusinessEntity",
                column: "rowguid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AK_BusinessEntityAddress_rowguid",
                schema: "Person",
                table: "BusinessEntityAddress",
                column: "rowguid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BusinessEntityAddress_AddressID",
                schema: "Person",
                table: "BusinessEntityAddress",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessEntityAddress_AddressTypeID",
                schema: "Person",
                table: "BusinessEntityAddress",
                column: "AddressTypeID");

            migrationBuilder.CreateIndex(
                name: "AK_BusinessEntityContact_rowguid",
                schema: "Person",
                table: "BusinessEntityContact",
                column: "rowguid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BusinessEntityContact_ContactTypeID",
                schema: "Person",
                table: "BusinessEntityContact",
                column: "ContactTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessEntityContact_PersonID",
                schema: "Person",
                table: "BusinessEntityContact",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "AK_ContactType_Name",
                schema: "Person",
                table: "ContactType",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AK_CountryRegion_Name",
                schema: "Person",
                table: "CountryRegion",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CountryRegionCurrency_CurrencyCode",
                schema: "Sales",
                table: "CountryRegionCurrency",
                column: "CurrencyCode");

            migrationBuilder.CreateIndex(
                name: "AK_CreditCard_CardNumber",
                schema: "Sales",
                table: "CreditCard",
                column: "CardNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AK_Culture_Name",
                schema: "Production",
                table: "Culture",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AK_Currency_Name",
                schema: "Sales",
                table: "Currency",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AK_CurrencyRate_CurrencyRateDate_FromCurrencyCode_ToCurrencyCode",
                schema: "Sales",
                table: "CurrencyRate",
                columns: new[] { "CurrencyRateDate", "FromCurrencyCode", "ToCurrencyCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyRate_FromCurrencyCode",
                schema: "Sales",
                table: "CurrencyRate",
                column: "FromCurrencyCode");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyRate_ToCurrencyCode",
                schema: "Sales",
                table: "CurrencyRate",
                column: "ToCurrencyCode");

            migrationBuilder.CreateIndex(
                name: "AK_Customer_AccountNumber",
                schema: "Sales",
                table: "Customer",
                column: "AccountNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AK_Customer_rowguid",
                schema: "Sales",
                table: "Customer",
                column: "rowguid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_PersonID",
                schema: "Sales",
                table: "Customer",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_StoreID",
                schema: "Sales",
                table: "Customer",
                column: "StoreID");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_TerritoryID",
                schema: "Sales",
                table: "Customer",
                column: "TerritoryID");

            migrationBuilder.CreateIndex(
                name: "AK_Department_Name",
                schema: "HumanResources",
                table: "Department",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmailAddress_EmailAddress",
                schema: "Person",
                table: "EmailAddress",
                column: "EmailAddress");

            migrationBuilder.CreateIndex(
                name: "AK_Employee_LoginID",
                schema: "HumanResources",
                table: "Employee",
                column: "LoginID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AK_Employee_NationalIDNumber",
                schema: "HumanResources",
                table: "Employee",
                column: "NationalIDNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AK_Employee_rowguid",
                schema: "HumanResources",
                table: "Employee",
                column: "rowguid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDepartmentHistory_DepartmentID",
                schema: "HumanResources",
                table: "EmployeeDepartmentHistory",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDepartmentHistory_ShiftID",
                schema: "HumanResources",
                table: "EmployeeDepartmentHistory",
                column: "ShiftID");

            migrationBuilder.CreateIndex(
                name: "IX_JobCandidate_BusinessEntityID",
                schema: "HumanResources",
                table: "JobCandidate",
                column: "BusinessEntityID");

            migrationBuilder.CreateIndex(
                name: "AK_Location_Name",
                schema: "Production",
                table: "Location",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PayrollDetail_BusinessEntityID",
                schema: "HumanResources",
                table: "PayrollDetail",
                column: "BusinessEntityID");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollDetail_PayrollId",
                schema: "HumanResources",
                table: "PayrollDetail",
                column: "PayrollId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollParameter_IsActive",
                schema: "HumanResources",
                table: "PayrollParameter",
                column: "IsActive",
                unique: true,
                filter: "[IsActive] = 1");

            migrationBuilder.CreateIndex(
                name: "AK_Person_rowguid",
                schema: "Person",
                table: "Person",
                column: "rowguid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Person_LastName_FirstName_MiddleName",
                schema: "Person",
                table: "Person",
                columns: new[] { "LastName", "FirstName", "MiddleName" });

            migrationBuilder.CreateIndex(
                name: "PXML_Person_AddContact",
                schema: "Person",
                table: "Person",
                column: "AdditionalContactInfo");

            migrationBuilder.CreateIndex(
                name: "PXML_Person_Demographics",
                schema: "Person",
                table: "Person",
                column: "Demographics");

            migrationBuilder.CreateIndex(
                name: "XMLPATH_Person_Demographics",
                schema: "Person",
                table: "Person",
                column: "Demographics");

            migrationBuilder.CreateIndex(
                name: "XMLPROPERTY_Person_Demographics",
                schema: "Person",
                table: "Person",
                column: "Demographics");

            migrationBuilder.CreateIndex(
                name: "XMLVALUE_Person_Demographics",
                schema: "Person",
                table: "Person",
                column: "Demographics");

            migrationBuilder.CreateIndex(
                name: "IX_PersonCreditCard_CreditCardID",
                schema: "Sales",
                table: "PersonCreditCard",
                column: "CreditCardID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonPhone_PhoneNumber",
                schema: "Person",
                table: "PersonPhone",
                column: "PhoneNumber");

            migrationBuilder.CreateIndex(
                name: "IX_PersonPhone_PhoneNumberTypeID",
                schema: "Person",
                table: "PersonPhone",
                column: "PhoneNumberTypeID");

            migrationBuilder.CreateIndex(
                name: "AK_Product_Name",
                schema: "Production",
                table: "Product",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AK_Product_ProductNumber",
                schema: "Production",
                table: "Product",
                column: "ProductNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AK_Product_rowguid",
                schema: "Production",
                table: "Product",
                column: "rowguid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductModelID",
                schema: "Production",
                table: "Product",
                column: "ProductModelID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductSubcategoryID",
                schema: "Production",
                table: "Product",
                column: "ProductSubcategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_SizeUnitMeasureCode",
                schema: "Production",
                table: "Product",
                column: "SizeUnitMeasureCode");

            migrationBuilder.CreateIndex(
                name: "IX_Product_WeightUnitMeasureCode",
                schema: "Production",
                table: "Product",
                column: "WeightUnitMeasureCode");

            migrationBuilder.CreateIndex(
                name: "AK_ProductCategory_Name",
                schema: "Production",
                table: "ProductCategory",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AK_ProductCategory_rowguid",
                schema: "Production",
                table: "ProductCategory",
                column: "rowguid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AK_ProductDescription_rowguid",
                schema: "Production",
                table: "ProductDescription",
                column: "rowguid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductInventory_LocationID",
                schema: "Production",
                table: "ProductInventory",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "AK_ProductModel_Name",
                schema: "Production",
                table: "ProductModel",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AK_ProductModel_rowguid",
                schema: "Production",
                table: "ProductModel",
                column: "rowguid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "PXML_ProductModel_CatalogDescription",
                schema: "Production",
                table: "ProductModel",
                column: "CatalogDescription");

            migrationBuilder.CreateIndex(
                name: "PXML_ProductModel_Instructions",
                schema: "Production",
                table: "ProductModel",
                column: "Instructions");

            migrationBuilder.CreateIndex(
                name: "IX_ProductModelIllustration_IllustrationID",
                schema: "Production",
                table: "ProductModelIllustration",
                column: "IllustrationID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductModelProductDescriptionCulture_CultureID",
                schema: "Production",
                table: "ProductModelProductDescriptionCulture",
                column: "CultureID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductModelProductDescriptionCulture_ProductDescriptionID",
                schema: "Production",
                table: "ProductModelProductDescriptionCulture",
                column: "ProductDescriptionID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductPhoto_ProductPhotoID",
                schema: "Production",
                table: "ProductProductPhoto",
                column: "ProductPhotoID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReview_ProductID_Name",
                schema: "Production",
                table: "ProductReview",
                columns: new[] { "ProductID", "ReviewerName" });

            migrationBuilder.CreateIndex(
                name: "AK_ProductSubcategory_Name",
                schema: "Production",
                table: "ProductSubcategory",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AK_ProductSubcategory_rowguid",
                schema: "Production",
                table: "ProductSubcategory",
                column: "rowguid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubcategory_ProductCategoryID",
                schema: "Production",
                table: "ProductSubcategory",
                column: "ProductCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVendor_BusinessEntityID",
                schema: "Purchasing",
                table: "ProductVendor",
                column: "BusinessEntityID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVendor_UnitMeasureCode",
                schema: "Purchasing",
                table: "ProductVendor",
                column: "UnitMeasureCode");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderDetail_ProductID",
                schema: "Purchasing",
                table: "PurchaseOrderDetail",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderHeader_EmployeeID",
                schema: "Purchasing",
                table: "PurchaseOrderHeader",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderHeader_ShipMethodID",
                schema: "Purchasing",
                table: "PurchaseOrderHeader",
                column: "ShipMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderHeader_VendorID",
                schema: "Purchasing",
                table: "PurchaseOrderHeader",
                column: "VendorID");

            migrationBuilder.CreateIndex(
                name: "AK_SalesOrderDetail_rowguid",
                schema: "Sales",
                table: "SalesOrderDetail",
                column: "rowguid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderDetail_ProductID",
                schema: "Sales",
                table: "SalesOrderDetail",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderDetail_SpecialOfferID_ProductID",
                schema: "Sales",
                table: "SalesOrderDetail",
                columns: new[] { "SpecialOfferID", "ProductID" });

            migrationBuilder.CreateIndex(
                name: "AK_SalesOrderHeader_rowguid",
                schema: "Sales",
                table: "SalesOrderHeader",
                column: "rowguid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AK_SalesOrderHeader_SalesOrderNumber",
                schema: "Sales",
                table: "SalesOrderHeader",
                column: "SalesOrderNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderHeader_BillToAddressID",
                schema: "Sales",
                table: "SalesOrderHeader",
                column: "BillToAddressID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderHeader_CreditCardID",
                schema: "Sales",
                table: "SalesOrderHeader",
                column: "CreditCardID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderHeader_CurrencyRateID",
                schema: "Sales",
                table: "SalesOrderHeader",
                column: "CurrencyRateID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderHeader_CustomerID",
                schema: "Sales",
                table: "SalesOrderHeader",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderHeader_SalesPersonID",
                schema: "Sales",
                table: "SalesOrderHeader",
                column: "SalesPersonID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderHeader_ShipMethodID",
                schema: "Sales",
                table: "SalesOrderHeader",
                column: "ShipMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderHeader_ShipToAddressID",
                schema: "Sales",
                table: "SalesOrderHeader",
                column: "ShipToAddressID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderHeader_TerritoryID",
                schema: "Sales",
                table: "SalesOrderHeader",
                column: "TerritoryID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderHeaderSalesReason_SalesReasonID",
                schema: "Sales",
                table: "SalesOrderHeaderSalesReason",
                column: "SalesReasonID");

            migrationBuilder.CreateIndex(
                name: "AK_SalesPerson_rowguid",
                schema: "Sales",
                table: "SalesPerson",
                column: "rowguid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesPerson_TerritoryID",
                schema: "Sales",
                table: "SalesPerson",
                column: "TerritoryID");

            migrationBuilder.CreateIndex(
                name: "AK_SalesPersonQuotaHistory_rowguid",
                schema: "Sales",
                table: "SalesPersonQuotaHistory",
                column: "rowguid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AK_SalesTaxRate_rowguid",
                schema: "Sales",
                table: "SalesTaxRate",
                column: "rowguid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AK_SalesTaxRate_StateProvinceID_TaxType",
                schema: "Sales",
                table: "SalesTaxRate",
                columns: new[] { "StateProvinceID", "TaxType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AK_SalesTerritory_Name",
                schema: "Sales",
                table: "SalesTerritory",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AK_SalesTerritory_rowguid",
                schema: "Sales",
                table: "SalesTerritory",
                column: "rowguid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesTerritory_CountryRegionCode",
                schema: "Sales",
                table: "SalesTerritory",
                column: "CountryRegionCode");

            migrationBuilder.CreateIndex(
                name: "AK_SalesTerritoryHistory_rowguid",
                schema: "Sales",
                table: "SalesTerritoryHistory",
                column: "rowguid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesTerritoryHistory_TerritoryID",
                schema: "Sales",
                table: "SalesTerritoryHistory",
                column: "TerritoryID");

            migrationBuilder.CreateIndex(
                name: "AK_ScrapReason_Name",
                schema: "Production",
                table: "ScrapReason",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AK_Shift_Name",
                schema: "HumanResources",
                table: "Shift",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AK_Shift_StartTime_EndTime",
                schema: "HumanResources",
                table: "Shift",
                columns: new[] { "StartTime", "EndTime" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AK_ShipMethod_Name",
                schema: "Purchasing",
                table: "ShipMethod",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AK_ShipMethod_rowguid",
                schema: "Purchasing",
                table: "ShipMethod",
                column: "rowguid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItem_ProductID",
                schema: "Sales",
                table: "ShoppingCartItem",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItem_ShoppingCartID_ProductID",
                schema: "Sales",
                table: "ShoppingCartItem",
                columns: new[] { "ShoppingCartID", "ProductID" });

            migrationBuilder.CreateIndex(
                name: "AK_SpecialOffer_rowguid",
                schema: "Sales",
                table: "SpecialOffer",
                column: "rowguid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AK_SpecialOfferProduct_rowguid",
                schema: "Sales",
                table: "SpecialOfferProduct",
                column: "rowguid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SpecialOfferProduct_ProductID",
                schema: "Sales",
                table: "SpecialOfferProduct",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "AK_StateProvince_Name",
                schema: "Person",
                table: "StateProvince",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AK_StateProvince_rowguid",
                schema: "Person",
                table: "StateProvince",
                column: "rowguid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AK_StateProvince_StateProvinceCode_CountryRegionCode",
                schema: "Person",
                table: "StateProvince",
                columns: new[] { "StateProvinceCode", "CountryRegionCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StateProvince_CountryRegionCode",
                schema: "Person",
                table: "StateProvince",
                column: "CountryRegionCode");

            migrationBuilder.CreateIndex(
                name: "IX_StateProvince_TerritoryID",
                schema: "Person",
                table: "StateProvince",
                column: "TerritoryID");

            migrationBuilder.CreateIndex(
                name: "AK_Store_rowguid",
                schema: "Sales",
                table: "Store",
                column: "rowguid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Store_SalesPersonID",
                schema: "Sales",
                table: "Store",
                column: "SalesPersonID");

            migrationBuilder.CreateIndex(
                name: "PXML_Store_Demographics",
                schema: "Sales",
                table: "Store",
                column: "Demographics");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHistory_ProductID",
                schema: "Production",
                table: "TransactionHistory",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHistory_ReferenceOrderID_ReferenceOrderLineID",
                schema: "Production",
                table: "TransactionHistory",
                columns: new[] { "ReferenceOrderID", "ReferenceOrderLineID" });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHistoryArchive_ProductID",
                schema: "Production",
                table: "TransactionHistoryArchive",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHistoryArchive_ReferenceOrderID_ReferenceOrderLineID",
                schema: "Production",
                table: "TransactionHistoryArchive",
                columns: new[] { "ReferenceOrderID", "ReferenceOrderLineID" });

            migrationBuilder.CreateIndex(
                name: "AK_UnitMeasure_Name",
                schema: "Production",
                table: "UnitMeasure",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AK_Vendor_AccountNumber",
                schema: "Purchasing",
                table: "Vendor",
                column: "AccountNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrder_ProductID",
                schema: "Production",
                table: "WorkOrder",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrder_ScrapReasonID",
                schema: "Production",
                table: "WorkOrder",
                column: "ScrapReasonID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderRouting_LocationID",
                schema: "Production",
                table: "WorkOrderRouting",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderRouting_ProductID",
                schema: "Production",
                table: "WorkOrderRouting",
                column: "ProductID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserPasskeys");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AWBuildVersion");

            migrationBuilder.DropTable(
                name: "BillOfMaterials",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "BusinessEntityAddress",
                schema: "Person");

            migrationBuilder.DropTable(
                name: "BusinessEntityContact",
                schema: "Person");

            migrationBuilder.DropTable(
                name: "CountryRegionCurrency",
                schema: "Sales");

            migrationBuilder.DropTable(
                name: "DatabaseLog");

            migrationBuilder.DropTable(
                name: "EmailAddress",
                schema: "Person");

            migrationBuilder.DropTable(
                name: "EmployeeDepartmentHistory",
                schema: "HumanResources");

            migrationBuilder.DropTable(
                name: "EmployeePayHistory",
                schema: "HumanResources");

            migrationBuilder.DropTable(
                name: "ErrorLog");

            migrationBuilder.DropTable(
                name: "JobCandidate",
                schema: "HumanResources");

            migrationBuilder.DropTable(
                name: "Password",
                schema: "Person");

            migrationBuilder.DropTable(
                name: "PayrollDetail",
                schema: "HumanResources");

            migrationBuilder.DropTable(
                name: "PayrollParameter",
                schema: "HumanResources");

            migrationBuilder.DropTable(
                name: "PersonCreditCard",
                schema: "Sales");

            migrationBuilder.DropTable(
                name: "PersonPhone",
                schema: "Person");

            migrationBuilder.DropTable(
                name: "ProductCostHistory",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "ProductInventory",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "ProductListPriceHistory",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "ProductModelIllustration",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "ProductModelProductDescriptionCulture",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "ProductProductPhoto",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "ProductReview",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "ProductVendor",
                schema: "Purchasing");

            migrationBuilder.DropTable(
                name: "PurchaseOrderDetail",
                schema: "Purchasing");

            migrationBuilder.DropTable(
                name: "SalesOrderDetail",
                schema: "Sales");

            migrationBuilder.DropTable(
                name: "SalesOrderHeaderSalesReason",
                schema: "Sales");

            migrationBuilder.DropTable(
                name: "SalesPersonQuotaHistory",
                schema: "Sales");

            migrationBuilder.DropTable(
                name: "SalesTaxRate",
                schema: "Sales");

            migrationBuilder.DropTable(
                name: "SalesTerritoryHistory",
                schema: "Sales");

            migrationBuilder.DropTable(
                name: "ShoppingCartItem",
                schema: "Sales");

            migrationBuilder.DropTable(
                name: "TransactionHistory",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "TransactionHistoryArchive",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "WorkOrderRouting",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AddressType",
                schema: "Person");

            migrationBuilder.DropTable(
                name: "ContactType",
                schema: "Person");

            migrationBuilder.DropTable(
                name: "Department",
                schema: "HumanResources");

            migrationBuilder.DropTable(
                name: "Shift",
                schema: "HumanResources");

            migrationBuilder.DropTable(
                name: "Payroll",
                schema: "HumanResources");

            migrationBuilder.DropTable(
                name: "PhoneNumberType",
                schema: "Person");

            migrationBuilder.DropTable(
                name: "Illustration",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "Culture",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "ProductDescription",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "ProductPhoto",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "PurchaseOrderHeader",
                schema: "Purchasing");

            migrationBuilder.DropTable(
                name: "SpecialOfferProduct",
                schema: "Sales");

            migrationBuilder.DropTable(
                name: "SalesOrderHeader",
                schema: "Sales");

            migrationBuilder.DropTable(
                name: "SalesReason",
                schema: "Sales");

            migrationBuilder.DropTable(
                name: "Location",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "WorkOrder",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "Vendor",
                schema: "Purchasing");

            migrationBuilder.DropTable(
                name: "SpecialOffer",
                schema: "Sales");

            migrationBuilder.DropTable(
                name: "Address",
                schema: "Person");

            migrationBuilder.DropTable(
                name: "CreditCard",
                schema: "Sales");

            migrationBuilder.DropTable(
                name: "CurrencyRate",
                schema: "Sales");

            migrationBuilder.DropTable(
                name: "Customer",
                schema: "Sales");

            migrationBuilder.DropTable(
                name: "ShipMethod",
                schema: "Purchasing");

            migrationBuilder.DropTable(
                name: "Product",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "ScrapReason",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "StateProvince",
                schema: "Person");

            migrationBuilder.DropTable(
                name: "Currency",
                schema: "Sales");

            migrationBuilder.DropTable(
                name: "Store",
                schema: "Sales");

            migrationBuilder.DropTable(
                name: "ProductModel",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "ProductSubcategory",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "UnitMeasure",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "SalesPerson",
                schema: "Sales");

            migrationBuilder.DropTable(
                name: "ProductCategory",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "Employee",
                schema: "HumanResources");

            migrationBuilder.DropTable(
                name: "SalesTerritory",
                schema: "Sales");

            migrationBuilder.DropTable(
                name: "Person",
                schema: "Person");

            migrationBuilder.DropTable(
                name: "CountryRegion",
                schema: "Person");

            migrationBuilder.DropTable(
                name: "BusinessEntity",
                schema: "Person");
        }
    }
}
