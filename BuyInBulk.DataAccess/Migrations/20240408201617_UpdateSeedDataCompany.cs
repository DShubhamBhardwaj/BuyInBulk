using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuyInBulk.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedDataCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PostalCode",
                table: "Companies",
                newName: "Pincode");

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "City", "State" },
                values: new object[] { "Delhi", "New Delhi" });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "City", "State" },
                values: new object[] { "Bangalore", "Karnatka" });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "City", "State" },
                values: new object[] { "Bangalore", "Karnatka" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Pincode",
                table: "Companies",
                newName: "PostalCode");

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "City", "State" },
                values: new object[] { "Tech City", "IL" });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "City", "State" },
                values: new object[] { "Vid City", "IL" });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "City", "State" },
                values: new object[] { "Lala land", "NY" });
        }
    }
}
