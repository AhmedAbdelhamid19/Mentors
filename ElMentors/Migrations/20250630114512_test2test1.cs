using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElMentors.Migrations
{
    /// <inheritdoc />
    public partial class test2test1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "child1s");

            migrationBuilder.DropTable(
                name: "child2s");

            migrationBuilder.DropTable(
                name: "MidTests");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Test2");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Test1");

            migrationBuilder.DropSequence(
                name: "parentSequence");

            migrationBuilder.AddColumn<int>(
                name: "Test2Id",
                table: "Test1",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Test1_Test2Id",
                table: "Test1",
                column: "Test2Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Test1_Test2_Test2Id",
                table: "Test1",
                column: "Test2Id",
                principalTable: "Test2",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Test1_Test2_Test2Id",
                table: "Test1");

            migrationBuilder.DropIndex(
                name: "IX_Test1_Test2Id",
                table: "Test1");

            migrationBuilder.DropColumn(
                name: "Test2Id",
                table: "Test1");

            migrationBuilder.CreateSequence(
                name: "parentSequence");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Test2",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Test1",
                type: "nvarchar(max)",
                nullable: true);

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

            migrationBuilder.CreateTable(
                name: "MidTests",
                columns: table => new
                {
                    Test1Id = table.Column<int>(type: "int", nullable: false),
                    Test2Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MidTests", x => new { x.Test1Id, x.Test2Id });
                    table.ForeignKey(
                        name: "FK_MidTests_Test1_Test1Id",
                        column: x => x.Test1Id,
                        principalTable: "Test1",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MidTests_Test2_Test2Id",
                        column: x => x.Test2Id,
                        principalTable: "Test2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MidTests_Test2Id",
                table: "MidTests",
                column: "Test2Id");
        }
    }
}
