using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICR_WEB_API.Service.Model.Migrations
{
    /// <inheritdoc />
    public partial class init4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Operator",
                table: "QuestionConditions",
                newName: "[Operator]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "[Operator]",
                table: "QuestionConditions",
                newName: "Operator");
        }
    }
}
