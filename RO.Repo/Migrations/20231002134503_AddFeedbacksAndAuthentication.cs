using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RO.Repo.Migrations
{
    /// <inheritdoc />
    public partial class AddFeedbacksAndAuthentication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedDate",
                table: "Recipes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedDate",
                table: "Ingredients",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FeedBack",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rate = table.Column<int>(type: "int", nullable: false),
                    Review = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedBack", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeedBack_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3b75240d-72ba-4f75-bbbf-d075f60f8ee4", "2", "User", "User" },
                    { "a7e4f24a-7eef-4027-a302-981544d4b132", "3", "HR", "HR" },
                    { "eed47edd-6a68-4355-b950-a7441e0998ed", "1", "Admin", "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeedBack_RecipeId",
                table: "FeedBack",
                column: "RecipeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeedBack");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3b75240d-72ba-4f75-bbbf-d075f60f8ee4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a7e4f24a-7eef-4027-a302-981544d4b132");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eed47edd-6a68-4355-b950-a7441e0998ed");

            migrationBuilder.DropColumn(
                name: "AddedDate",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "AddedDate",
                table: "Ingredients");

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
    }
}
