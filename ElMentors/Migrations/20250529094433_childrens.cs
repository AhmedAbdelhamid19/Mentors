using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElMentors.Migrations
{
    /// <inheritdoc />
    public partial class childrens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_parents",
                table: "parents");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "parents");

            migrationBuilder.RenameTable(
                name: "parents",
                newName: "child1s");

            migrationBuilder.AddPrimaryKey(
                name: "PK_child1s",
                table: "child1s",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "child2s",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Child2Property = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_child2s", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "child2s");

            migrationBuilder.DropPrimaryKey(
                name: "PK_child1s",
                table: "child1s");

            migrationBuilder.RenameTable(
                name: "child1s",
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
    }
}
