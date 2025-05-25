using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElMentors.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Test");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Test2",
                newName: "Name");

            migrationBuilder.CreateTable(
                name: "Test1",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test1", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Test1Test2",
                columns: table => new
                {
                    Tests1Id = table.Column<int>(type: "int", nullable: false),
                    Tests2Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test1Test2", x => new { x.Tests1Id, x.Tests2Id });
                    table.ForeignKey(
                        name: "FK_Test1Test2_Test1_Tests1Id",
                        column: x => x.Tests1Id,
                        principalTable: "Test1",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Test1Test2_Test2_Tests2Id",
                        column: x => x.Tests2Id,
                        principalTable: "Test2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Test1Test2_Tests2Id",
                table: "Test1Test2",
                column: "Tests2Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Test1Test2");

            migrationBuilder.DropTable(
                name: "Test1");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Test2",
                newName: "Description");

            migrationBuilder.CreateTable(
                name: "Test",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Test2FK = table.Column<int>(type: "int", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    zong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Speed = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Test_Test2_Test2FK",
                        column: x => x.Test2FK,
                        principalTable: "Test2",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Test_Test2FK",
                table: "Test",
                column: "Test2FK");
        }
    }
}
