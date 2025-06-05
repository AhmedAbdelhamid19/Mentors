using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElMentors.Migrations
{
    /// <inheritdoc />
    public partial class parents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_childs",
                table: "childs");

            migrationBuilder.RenameTable(
                name: "childs",
                newName: "parents");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "parents",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_parents",
                table: "parents",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_parents",
                table: "parents");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "parents");

            migrationBuilder.RenameTable(
                name: "parents",
                newName: "childs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_childs",
                table: "childs",
                column: "Id");
        }
    }
}
