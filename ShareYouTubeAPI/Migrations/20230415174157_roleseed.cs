using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShareYouTubeAPI.Migrations
{
    /// <inheritdoc />
    public partial class roleseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "52451663-164f-4676-9d7f-2f3fe5c51377");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5a0defa7-94bc-4a0b-b558-4dee8c649923");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "01e68244-6c20-4411-9c71-39fbcf1fcc64", "2", "User", "User" },
                    { "cdcc11d3-bb21-4753-84f7-6564ad925ad3", "1", "Admin", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01e68244-6c20-4411-9c71-39fbcf1fcc64");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cdcc11d3-bb21-4753-84f7-6564ad925ad3");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "52451663-164f-4676-9d7f-2f3fe5c51377", "1", "Admin", "Administrator" },
                    { "5a0defa7-94bc-4a0b-b558-4dee8c649923", "2", "User", "Ostali korisnik" }
                });
        }
    }
}
