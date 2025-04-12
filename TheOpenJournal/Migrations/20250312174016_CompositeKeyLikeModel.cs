using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TheOpenJournal.Migrations
{
    public partial class CompositeKeyLikeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Like",
                table: "Like");

            migrationBuilder.DropIndex(
                name: "IX_Like_PostId",
                table: "Like");

            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "Id",
                keyValue: new Guid("717a6d12-ab22-492a-86eb-469ceede5013"));

            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "Id",
                keyValue: new Guid("eb0d69e5-5ae0-45c2-a101-0efef794ae2d"));

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Like");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Like",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Like",
                table: "Like",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "Id", "Email", "FullName", "IsActive", "Password", "PhoneNumber", "ProfileUrl", "Username" },
                values: new object[,]
                {
                    { new Guid("717a6d12-ab22-492a-86eb-469ceede5013"), "raju@gmail.com", "Raju", true, "AQAAAAEAACcQAAAAEKZ7+buT9BkPId1PW8inI8YbEksr+OTCGB5L7V15YXhdN0OebXjubyEIMWmAocbbdA==", "9745868539", null, "Rajuxz" },
                    { new Guid("eb0d69e5-5ae0-45c2-a101-0efef794ae2d"), "admin@gmail.com", "Admin", true, "AQAAAAEAACcQAAAAEMK3L38WjX90uvZ5Tq+Np7AOu0Y9XFzh/vFWp8dDBp6xsJ2nxUidoRib4mnlzclVKQ==", "9814964044", null, "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Like_PostId",
                table: "Like",
                column: "PostId");
        }
    }
}
