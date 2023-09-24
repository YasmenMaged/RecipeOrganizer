using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RO.Repo.Migrations
{
    /// <inheritdoc />
    public partial class RolesSeeded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1df0afb4-4dd4-45ab-a4a7-df82dce9c239", "2", "User", "User" },
                    { "376cba5c-43a5-4a6f-921d-c313bb401b46", "3", "HR", "HR" },
                    { "a24c69c9-7156-489d-9160-e8796eb19e31", "1", "Admin", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1df0afb4-4dd4-45ab-a4a7-df82dce9c239");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "376cba5c-43a5-4a6f-921d-c313bb401b46");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a24c69c9-7156-489d-9160-e8796eb19e31");
        }
    }
}
