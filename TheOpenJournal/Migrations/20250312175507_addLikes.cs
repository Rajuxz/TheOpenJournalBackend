using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheOpenJournal.Migrations
{
    public partial class addLikes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Like_AspNetUsers_UserId",
                table: "Like");

            migrationBuilder.DropForeignKey(
                name: "FK_Like_Posts_PostId",
                table: "Like");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Like",
                table: "Like");

            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "Id",
                keyValue: new Guid("1c39a4d4-7ee3-4570-b53b-3e8e859a47fb"));

            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "Id",
                keyValue: new Guid("bfb682e4-9f49-4324-9628-0f99d6b93390"));

            migrationBuilder.RenameTable(
                name: "Like",
                newName: "Likes");

            migrationBuilder.RenameIndex(
                name: "IX_Like_UserId",
                table: "Likes",
                newName: "IX_Likes_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Likes",
                table: "Likes",
                columns: new[] { "PostId", "UserId" });

            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "Id", "Email", "FullName", "IsActive", "Password", "PhoneNumber", "ProfileUrl", "Username" },
                values: new object[,]
                {
                    { new Guid("25168bdb-a90e-44cb-a90e-8f95d613a42c"), "raju@gmail.com", "Raju", true, "AQAAAAEAACcQAAAAEJn6mS8ic8q3CeTbGkd6G5PvgEp2XRuDUKFzP9rVLDGbZtkt8GiWkbmZ3JxE/vdKXg==", "9745868539", null, "Rajuxz" },
                    { new Guid("b54dc79a-711d-441b-b496-3170ff5270bf"), "admin@gmail.com", "Admin", true, "AQAAAAEAACcQAAAAEI88ldINAJQczgFYdEp/Gq4dycfWr9E8QAzDFMow1Gv4M9YIYkY8NadSkRaguDA2XQ==", "9814964044", null, "Admin" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_AspNetUsers_UserId",
                table: "Likes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Posts_PostId",
                table: "Likes",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_AspNetUsers_UserId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Posts_PostId",
                table: "Likes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Likes",
                table: "Likes");

            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "Id",
                keyValue: new Guid("25168bdb-a90e-44cb-a90e-8f95d613a42c"));

            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "Id",
                keyValue: new Guid("b54dc79a-711d-441b-b496-3170ff5270bf"));

            migrationBuilder.RenameTable(
                name: "Likes",
                newName: "Like");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_UserId",
                table: "Like",
                newName: "IX_Like_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Like",
                table: "Like",
                columns: new[] { "PostId", "UserId" });

            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "Id", "Email", "FullName", "IsActive", "Password", "PhoneNumber", "ProfileUrl", "Username" },
                values: new object[,]
                {
                    { new Guid("1c39a4d4-7ee3-4570-b53b-3e8e859a47fb"), "raju@gmail.com", "Raju", true, "AQAAAAEAACcQAAAAEM6Y0Z3S1TckMd3Yt1qDOdM2h6qf+BLsTvML5P3agZC+UK+k2z76xxkP3DC+uLlJyw==", "9745868539", null, "Rajuxz" },
                    { new Guid("bfb682e4-9f49-4324-9628-0f99d6b93390"), "admin@gmail.com", "Admin", true, "AQAAAAEAACcQAAAAECOJDywp/FwQ3u/RrpGha/h1e8g5tnejvNRe3exl0IGOm5oQvbAD8GYZ9hR3aJ8BRQ==", "9814964044", null, "Admin" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Like_AspNetUsers_UserId",
                table: "Like",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Like_Posts_PostId",
                table: "Like",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
