using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class makeShortUrlUserIdnonprimarykey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ShortUrls",
                table: "ShortUrls");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShortUrls",
                table: "ShortUrls",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ShortUrls",
                table: "ShortUrls");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShortUrls",
                table: "ShortUrls",
                column: "UserId");
        }
    }
}
