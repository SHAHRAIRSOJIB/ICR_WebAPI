using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICR_WEB_API.Service.Model.Migrations
{
    /// <inheritdoc />
    public partial class fromAIUB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageBase64",
                table: "Responses",
                newName: "imageBase64");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "imageBase64",
                table: "Responses",
                newName: "ImageBase64");
        }
    }
}
