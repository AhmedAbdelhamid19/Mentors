using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElMentors.Migrations
{
    /// <inheritdoc />
    public partial class midManytoMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JointTests");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MidTests");

            migrationBuilder.CreateTable(
                name: "JointTests",
                columns: table => new
                {
                    Tests1Id = table.Column<int>(type: "int", nullable: false),
                    Tests2Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JointTests", x => new { x.Tests1Id, x.Tests2Id });
                    table.ForeignKey(
                        name: "FK_JointTests_Test1_Tests1Id",
                        column: x => x.Tests1Id,
                        principalTable: "Test1",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JointTests_Test2_Tests2Id",
                        column: x => x.Tests2Id,
                        principalTable: "Test2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JointTests_Tests2Id",
                table: "JointTests",
                column: "Tests2Id");
        }
    }
}
