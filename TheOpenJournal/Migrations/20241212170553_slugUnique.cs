using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheOpenJournal.Migrations
{
    public partial class slugUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "Id",
                keyValue: new Guid("45c9fe40-255e-44c7-b9ad-2cc1bb5e12fc"));

            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "Id",
                keyValue: new Guid("708edfdf-60b7-4de8-823b-39a1cb515fce"));

            migrationBuilder.AlterColumn<int>(
                name: "UniqueViewCount",
                table: "Posts",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LikeCount",
                table: "Posts",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CommentCount",
                table: "Posts",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "Id", "Email", "FullName", "IsActive", "Password", "PhoneNumber", "ProfileUrl", "Username" },
                values: new object[,]
                {
                    { new Guid("edaace10-89cc-4520-8fe0-73db7c1b2ded"), "raju@gmail.com", "Raju", true, "R@ju_1", "9745868539", null, "Rajuxz" },
                    { new Guid("fa30d24a-99c8-4356-944b-7c899d304a89"), "admin@gmail.com", "Admin", true, "12345", "9814964044", null, "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_Slug",
                table: "Posts",
                column: "Slug",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Posts_Slug",
                table: "Posts");

            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "Id",
                keyValue: new Guid("edaace10-89cc-4520-8fe0-73db7c1b2ded"));

            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "Id",
                keyValue: new Guid("fa30d24a-99c8-4356-944b-7c899d304a89"));

            migrationBuilder.AlterColumn<int>(
                name: "UniqueViewCount",
                table: "Posts",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "LikeCount",
                table: "Posts",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "CommentCount",
                table: "Posts",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "Id", "Email", "FullName", "IsActive", "Password", "PhoneNumber", "ProfileUrl", "Username" },
                values: new object[,]
                {
                    { new Guid("45c9fe40-255e-44c7-b9ad-2cc1bb5e12fc"), "admin@gmail.com", "Admin", true, "12345", "9814964044", null, "Admin" },
                    { new Guid("708edfdf-60b7-4de8-823b-39a1cb515fce"), "raju@gmail.com", "Raju", true, "R@ju_1", "9745868539", null, "Rajuxz" }
                });
        }
    }
}
