using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShareYouTubeAPI.Migrations
{
    /// <inheritdoc />
    public partial class migratio_models_appuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "50dbd20a-cadd-4e00-8d67-ac29b7408d62", "1", "Admin", "Admin" },
                    { "8b2771b8-418f-4ce0-b5c4-bf4a79dd4997", "2", "User", "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "50dbd20a-cadd-4e00-8d67-ac29b7408d62");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8b2771b8-418f-4ce0-b5c4-bf4a79dd4997");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "31d8969f-f422-474e-8103-17a4144b4ae8", "1", "Admin", "Admin" },
                    { "7901a6fb-3846-4d4c-8117-b0f8771b59ca", "2", "User", "User" }
                });
        }
    }
}
