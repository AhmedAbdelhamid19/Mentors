using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElMentors.Migrations
{
    /// <inheritdoc />
    public partial class tpc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "parentSequence");

            migrationBuilder.CreateTable(
                name: "child1s",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [parentSequence]"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Child1Property = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_child1s", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "child2s",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [parentSequence]"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Child2Property = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "child1s");

            migrationBuilder.DropTable(
                name: "child2s");

            migrationBuilder.DropSequence(
                name: "parentSequence");
        }
    }
}
