using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheOpenJournal.Migrations
{
    public partial class addRelationshipWithUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "Id",
                keyValue: new Guid("0b384ff3-e93b-42fd-b02f-e1f228282ae5"));

            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "Id",
                keyValue: new Guid("358ac56d-3477-46df-bd7c-0eb8cf2efe78"));

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Posts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "Id", "Email", "FullName", "IsActive", "Password", "PhoneNumber", "ProfileUrl", "Username" },
                values: new object[,]
                {
                    { new Guid("8eecc5f1-f7e7-4e7c-a7e3-c9831a95c48a"), "admin@gmail.com", "Admin", true, "12345", "9814964044", null, "Admin" },
                    { new Guid("a0bcb57c-9c77-415d-93eb-96fcc453739c"), "raju@gmail.com", "Raju", true, "R@ju_1", "9745868539", null, "Rajuxz" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserId",
                table: "Posts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_UserId",
                table: "Posts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_UserId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_UserId",
                table: "Posts");

            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "Id",
                keyValue: new Guid("8eecc5f1-f7e7-4e7c-a7e3-c9831a95c48a"));

            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "Id",
                keyValue: new Guid("a0bcb57c-9c77-415d-93eb-96fcc453739c"));

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Posts");

            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "Id", "Email", "FullName", "IsActive", "Password", "PhoneNumber", "ProfileUrl", "Username" },
                values: new object[,]
                {
                    { new Guid("0b384ff3-e93b-42fd-b02f-e1f228282ae5"), "admin@gmail.com", "Admin", true, "12345", "9814964044", null, "Admin" },
                    { new Guid("358ac56d-3477-46df-bd7c-0eb8cf2efe78"), "raju@gmail.com", "Raju", true, "R@ju_1", "9745868539", null, "Rajuxz" }
                });
        }
    }
}
