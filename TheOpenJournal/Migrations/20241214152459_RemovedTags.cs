using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheOpenJournal.Migrations
{
    public partial class RemovedTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostTag");

            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "Id",
                keyValue: new Guid("7b38c556-ed71-4cb4-834f-86d87d4cd829"));

            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "Id",
                keyValue: new Guid("9a19562d-615d-40a3-b034-9e098c90a58d"));

            migrationBuilder.AddColumn<int>(
                name: "TagId",
                table: "Posts",
                type: "integer",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "Id", "Email", "FullName", "IsActive", "Password", "PhoneNumber", "ProfileUrl", "Username" },
                values: new object[,]
                {
                    { new Guid("0b384ff3-e93b-42fd-b02f-e1f228282ae5"), "admin@gmail.com", "Admin", true, "12345", "9814964044", null, "Admin" },
                    { new Guid("358ac56d-3477-46df-bd7c-0eb8cf2efe78"), "raju@gmail.com", "Raju", true, "R@ju_1", "9745868539", null, "Rajuxz" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_TagId",
                table: "Posts",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Tags_TagId",
                table: "Posts",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Tags_TagId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_TagId",
                table: "Posts");

            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "Id",
                keyValue: new Guid("0b384ff3-e93b-42fd-b02f-e1f228282ae5"));

            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "Id",
                keyValue: new Guid("358ac56d-3477-46df-bd7c-0eb8cf2efe78"));

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "Posts");

            migrationBuilder.CreateTable(
                name: "PostTag",
                columns: table => new
                {
                    PostsId = table.Column<Guid>(type: "uuid", nullable: false),
                    TagsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTag", x => new { x.PostsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_PostTag_Posts_PostsId",
                        column: x => x.PostsId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "Id", "Email", "FullName", "IsActive", "Password", "PhoneNumber", "ProfileUrl", "Username" },
                values: new object[,]
                {
                    { new Guid("7b38c556-ed71-4cb4-834f-86d87d4cd829"), "admin@gmail.com", "Admin", true, "12345", "9814964044", null, "Admin" },
                    { new Guid("9a19562d-615d-40a3-b034-9e098c90a58d"), "raju@gmail.com", "Raju", true, "R@ju_1", "9745868539", null, "Rajuxz" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostTag_TagsId",
                table: "PostTag",
                column: "TagsId");
        }
    }
}
