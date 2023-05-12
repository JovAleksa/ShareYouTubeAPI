using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShareYouTubeAPI.Migrations
{
    /// <inheritdoc />
    public partial class migratio_models : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "31d8969f-f422-474e-8103-17a4144b4ae8", "1", "Admin", "Admin" },
                    { "7901a6fb-3846-4d4c-8117-b0f8771b59ca", "2", "User", "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "31d8969f-f422-474e-8103-17a4144b4ae8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7901a6fb-3846-4d4c-8117-b0f8771b59ca");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "01e68244-6c20-4411-9c71-39fbcf1fcc64", "2", "User", "User" },
                    { "cdcc11d3-bb21-4753-84f7-6564ad925ad3", "1", "Admin", "Admin" }
                });
        }
    }
}
