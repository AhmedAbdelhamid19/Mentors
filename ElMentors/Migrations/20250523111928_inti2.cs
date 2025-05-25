using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElMentors.Migrations
{
    /// <inheritdoc />
    public partial class inti2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "zing",
                table: "Child",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "zing",
                table: "Child");
        }
    }
}
