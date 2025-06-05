using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElMentors.Migrations
{
    /// <inheritdoc />
    public partial class family : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "child1s");

            migrationBuilder.DropPrimaryKey(
                name: "PK_child2s",
                table: "child2s");

            migrationBuilder.RenameTable(
                name: "child2s",
                newName: "Parents");

            migrationBuilder.AddColumn<string>(
                name: "Child1Property",
                table: "Parents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Parents",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Parents",
                table: "Parents",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Parents",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "Child1Property",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Parents");

            migrationBuilder.RenameTable(
                name: "Parents",
                newName: "child2s");

            migrationBuilder.AddPrimaryKey(
                name: "PK_child2s",
                table: "child2s",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "child1s",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Child1Property = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_child1s", x => x.Id);
                });
        }
    }
}
