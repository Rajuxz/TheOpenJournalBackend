using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheOpenJournal.Migrations
{
    public partial class updateColumnTypeMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "Id",
                keyValue: new Guid("8eecc5f1-f7e7-4e7c-a7e3-c9831a95c48a"));

            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "Id",
                keyValue: new Guid("a0bcb57c-9c77-415d-93eb-96fcc453739c"));

            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "Id", "Email", "FullName", "IsActive", "Password", "PhoneNumber", "ProfileUrl", "Username" },
                values: new object[,]
                {
                    { new Guid("00e21a92-4d7f-4035-92d2-7ff2158d81d9"), "admin@gmail.com", "Admin", true, "12345", "9814964044", null, "Admin" },
                    { new Guid("e92c98de-b2ba-4ccc-a8fc-84de26138ecd"), "raju@gmail.com", "Raju", true, "R@ju_1", "9745868539", null, "Rajuxz" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "Id",
                keyValue: new Guid("00e21a92-4d7f-4035-92d2-7ff2158d81d9"));

            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "Id",
                keyValue: new Guid("e92c98de-b2ba-4ccc-a8fc-84de26138ecd"));

            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "Id", "Email", "FullName", "IsActive", "Password", "PhoneNumber", "ProfileUrl", "Username" },
                values: new object[,]
                {
                    { new Guid("8eecc5f1-f7e7-4e7c-a7e3-c9831a95c48a"), "admin@gmail.com", "Admin", true, "12345", "9814964044", null, "Admin" },
                    { new Guid("a0bcb57c-9c77-415d-93eb-96fcc453739c"), "raju@gmail.com", "Raju", true, "R@ju_1", "9745868539", null, "Rajuxz" }
                });
        }
    }
}
