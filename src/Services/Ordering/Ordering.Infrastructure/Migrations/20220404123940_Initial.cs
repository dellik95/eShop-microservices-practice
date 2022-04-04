using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ordering.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CardName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardExpiration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CVV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentType = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "AddressLine", "CVV", "CardExpiration", "CardName", "CardNumber", "Country", "CreatedBy", "CreatedOn", "EmailAddress", "FirstName", "LastName", "ModifiedBy", "ModifiedOn", "PaymentType", "State", "TotalPrice", "UserName", "ZipCode" },
                values: new object[] { 1, "Test", null, null, null, null, "Test", "swn", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test@gmail.com", "Test", "Test", "swn", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, 350m, "swn", null });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "AddressLine", "CVV", "CardExpiration", "CardName", "CardNumber", "Country", "CreatedBy", "CreatedOn", "EmailAddress", "FirstName", "LastName", "ModifiedBy", "ModifiedOn", "PaymentType", "State", "TotalPrice", "UserName", "ZipCode" },
                values: new object[] { 2, "Test", null, null, null, null, "Test", "swn", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test1@gmail.com", "Test1", "Test1", "swn", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, 350m, "swn1", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
