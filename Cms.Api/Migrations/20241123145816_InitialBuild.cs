using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cms.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialBuild : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemLanguages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LanguageCode = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemLanguages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryLanguages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    LanguageId = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryLanguages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryLanguages_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryLanguages_SystemLanguages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "SystemLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contents_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contents_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContentLanguages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    VariantId = table.Column<Guid>(type: "uuid", nullable: false),
                    ContentId = table.Column<int>(type: "integer", nullable: false),
                    LanguageId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentLanguages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentLanguages_Contents_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContentLanguages_SystemLanguages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "SystemLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContentVariantHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    VariantId = table.Column<Guid>(type: "uuid", nullable: false),
                    ContentId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentVariantHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentVariantHistories_Contents_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContentVariantHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 23, 14, 58, 16, 12, DateTimeKind.Utc).AddTicks(6629), new DateTime(2024, 11, 23, 14, 58, 16, 12, DateTimeKind.Utc).AddTicks(6630) },
                    { 2, new DateTime(2024, 11, 23, 14, 58, 16, 12, DateTimeKind.Utc).AddTicks(6632), new DateTime(2024, 11, 23, 14, 58, 16, 12, DateTimeKind.Utc).AddTicks(6633) },
                    { 3, new DateTime(2024, 11, 23, 14, 58, 16, 12, DateTimeKind.Utc).AddTicks(6633), new DateTime(2024, 11, 23, 14, 58, 16, 12, DateTimeKind.Utc).AddTicks(6633) },
                    { 4, new DateTime(2024, 11, 23, 14, 58, 16, 12, DateTimeKind.Utc).AddTicks(6634), new DateTime(2024, 11, 23, 14, 58, 16, 12, DateTimeKind.Utc).AddTicks(6634) }
                });

            migrationBuilder.InsertData(
                table: "SystemLanguages",
                columns: new[] { "Id", "LanguageCode" },
                values: new object[,]
                {
                    { 1, "tr-TR" },
                    { 2, "en-US" }
                });

            migrationBuilder.InsertData(
                table: "CategoryLanguages",
                columns: new[] { "Id", "CategoryId", "LanguageId", "Title" },
                values: new object[,]
                {
                    { 1, 1, 1, "Teknoloji" },
                    { 2, 1, 2, "Technology" },
                    { 3, 2, 1, "Sağlık" },
                    { 4, 2, 2, "Health" },
                    { 5, 3, 1, "Eğitim" },
                    { 6, 3, 2, "Education" },
                    { 7, 4, 1, "Ulaşım" },
                    { 8, 4, 2, "Transportation" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryLanguages_CategoryId_LanguageId",
                table: "CategoryLanguages",
                columns: new[] { "CategoryId", "LanguageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CategoryLanguages_LanguageId",
                table: "CategoryLanguages",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentLanguages_ContentId_LanguageId_VariantId",
                table: "ContentLanguages",
                columns: new[] { "ContentId", "LanguageId", "VariantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContentLanguages_LanguageId",
                table: "ContentLanguages",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Contents_CategoryId",
                table: "Contents",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Contents_UserId",
                table: "Contents",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentVariantHistories_ContentId",
                table: "ContentVariantHistories",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentVariantHistories_UserId_ContentId",
                table: "ContentVariantHistories",
                columns: new[] { "UserId", "ContentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContentVariantHistories_UserId_ContentId_VariantId",
                table: "ContentVariantHistories",
                columns: new[] { "UserId", "ContentId", "VariantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SystemLanguages_LanguageCode",
                table: "SystemLanguages",
                column: "LanguageCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryLanguages");

            migrationBuilder.DropTable(
                name: "ContentLanguages");

            migrationBuilder.DropTable(
                name: "ContentVariantHistories");

            migrationBuilder.DropTable(
                name: "SystemLanguages");

            migrationBuilder.DropTable(
                name: "Contents");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
