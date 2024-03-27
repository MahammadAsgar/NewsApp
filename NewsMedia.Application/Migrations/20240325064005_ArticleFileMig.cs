using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsMedia.Application.Migrations
{
    /// <inheritdoc />
    public partial class ArticleFileMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_AspNetUsers_AppUserId",
                table: "Articles");

            migrationBuilder.DropTable(
                name: "ArticleFiles");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "Articles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ArticleTitleFileId",
                table: "Articles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ArticleContentFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleId = table.Column<int>(type: "int", nullable: false),
                    AddDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleContentFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleContentFiles_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArticleTitleFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleTitleFiles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ArticleTitleFileId",
                table: "Articles",
                column: "ArticleTitleFileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ArticleContentFiles_ArticleId",
                table: "ArticleContentFiles",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_ArticleTitleFiles_ArticleTitleFileId",
                table: "Articles",
                column: "ArticleTitleFileId",
                principalTable: "ArticleTitleFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_AspNetUsers_AppUserId",
                table: "Articles",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_ArticleTitleFiles_ArticleTitleFileId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Articles_AspNetUsers_AppUserId",
                table: "Articles");

            migrationBuilder.DropTable(
                name: "ArticleContentFiles");

            migrationBuilder.DropTable(
                name: "ArticleTitleFiles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_ArticleTitleFileId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "ArticleTitleFileId",
                table: "Articles");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "Articles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "ArticleFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleId = table.Column<int>(type: "int", nullable: false),
                    AddDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArticleId1 = table.Column<int>(type: "int", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleFiles_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleFiles_Articles_ArticleId1",
                        column: x => x.ArticleId1,
                        principalTable: "Articles",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 3, null, "User", "USER" });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleFiles_ArticleId",
                table: "ArticleFiles",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleFiles_ArticleId1",
                table: "ArticleFiles",
                column: "ArticleId1",
                unique: true,
                filter: "[ArticleId1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_AspNetUsers_AppUserId",
                table: "Articles",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
