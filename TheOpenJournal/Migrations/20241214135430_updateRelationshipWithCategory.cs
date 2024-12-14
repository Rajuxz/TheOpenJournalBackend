using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheOpenJournal.Migrations
{
    public partial class updateRelationshipWithCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Categories_CategoryId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_CategoryId",
                table: "Posts");

            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "Id",
                keyValue: new Guid("edaace10-89cc-4520-8fe0-73db7c1b2ded"));

            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "Id",
                keyValue: new Guid("fa30d24a-99c8-4356-944b-7c899d304a89"));

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Posts");

            migrationBuilder.CreateTable(
                name: "CategoryPost",
                columns: table => new
                {
                    CategoriesId = table.Column<Guid>(type: "uuid", nullable: false),
                    PostsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryPost", x => new { x.CategoriesId, x.PostsId });
                    table.ForeignKey(
                        name: "FK_CategoryPost_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryPost_Posts_PostsId",
                        column: x => x.PostsId,
                        principalTable: "Posts",
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
                name: "IX_CategoryPost_PostsId",
                table: "CategoryPost",
                column: "PostsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryPost");

            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "Id",
                keyValue: new Guid("7b38c556-ed71-4cb4-834f-86d87d4cd829"));

            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "Id",
                keyValue: new Guid("9a19562d-615d-40a3-b034-9e098c90a58d"));

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Posts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "Id", "Email", "FullName", "IsActive", "Password", "PhoneNumber", "ProfileUrl", "Username" },
                values: new object[,]
                {
                    { new Guid("edaace10-89cc-4520-8fe0-73db7c1b2ded"), "raju@gmail.com", "Raju", true, "R@ju_1", "9745868539", null, "Rajuxz" },
                    { new Guid("fa30d24a-99c8-4356-944b-7c899d304a89"), "admin@gmail.com", "Admin", true, "12345", "9814964044", null, "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CategoryId",
                table: "Posts",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Categories_CategoryId",
                table: "Posts",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
