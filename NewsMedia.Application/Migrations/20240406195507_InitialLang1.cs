using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsMedia.Application.Migrations
{
    /// <inheritdoc />
    public partial class InitialLang1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Language",
                table: "Tags",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Language",
                table: "CategoryBases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Language",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Language",
                table: "Articles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Language",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "CategoryBases");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Articles");
        }
    }
}
