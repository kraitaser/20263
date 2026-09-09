using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _20263.Migrations
{
    /// <inheritdoc />
    public partial class categoriascontroller : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_categories",
                table: "categories");

            migrationBuilder.RenameTable(
                name: "categories",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_categories_Name",
                table: "Categories",
                newName: "IX_Categories_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "categories");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_Name",
                table: "categories",
                newName: "IX_categories_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_categories",
                table: "categories",
                column: "Id");
        }
    }
}
