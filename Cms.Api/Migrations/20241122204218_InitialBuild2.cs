using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cms.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialBuild2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ContentLanguages_ContentId_LanguageId",
                table: "ContentLanguages");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 22, 20, 42, 18, 445, DateTimeKind.Utc).AddTicks(6281), new DateTime(2024, 11, 22, 20, 42, 18, 445, DateTimeKind.Utc).AddTicks(6283) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 22, 20, 42, 18, 445, DateTimeKind.Utc).AddTicks(6286), new DateTime(2024, 11, 22, 20, 42, 18, 445, DateTimeKind.Utc).AddTicks(6286) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 22, 20, 42, 18, 445, DateTimeKind.Utc).AddTicks(6287), new DateTime(2024, 11, 22, 20, 42, 18, 445, DateTimeKind.Utc).AddTicks(6287) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 22, 20, 42, 18, 445, DateTimeKind.Utc).AddTicks(6288), new DateTime(2024, 11, 22, 20, 42, 18, 445, DateTimeKind.Utc).AddTicks(6288) });

            migrationBuilder.CreateIndex(
                name: "IX_ContentLanguages_ContentId_LanguageId_VariantId",
                table: "ContentLanguages",
                columns: new[] { "ContentId", "LanguageId", "VariantId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ContentLanguages_ContentId_LanguageId_VariantId",
                table: "ContentLanguages");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 22, 20, 39, 24, 319, DateTimeKind.Utc).AddTicks(5679), new DateTime(2024, 11, 22, 20, 39, 24, 319, DateTimeKind.Utc).AddTicks(5685) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 22, 20, 39, 24, 319, DateTimeKind.Utc).AddTicks(5687), new DateTime(2024, 11, 22, 20, 39, 24, 319, DateTimeKind.Utc).AddTicks(5687) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 22, 20, 39, 24, 319, DateTimeKind.Utc).AddTicks(5688), new DateTime(2024, 11, 22, 20, 39, 24, 319, DateTimeKind.Utc).AddTicks(5688) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 22, 20, 39, 24, 319, DateTimeKind.Utc).AddTicks(5689), new DateTime(2024, 11, 22, 20, 39, 24, 319, DateTimeKind.Utc).AddTicks(5690) });

            migrationBuilder.CreateIndex(
                name: "IX_ContentLanguages_ContentId_LanguageId",
                table: "ContentLanguages",
                columns: new[] { "ContentId", "LanguageId" },
                unique: true);
        }
    }
}
