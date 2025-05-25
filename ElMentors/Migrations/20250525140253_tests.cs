using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElMentors.Migrations
{
    /// <inheritdoc />
    public partial class tests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Test1Test2_Test1_Tests1Id",
                table: "Test1Test2");

            migrationBuilder.DropForeignKey(
                name: "FK_Test1Test2_Test2_Tests2Id",
                table: "Test1Test2");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Test1Test2",
                table: "Test1Test2");

            migrationBuilder.RenameTable(
                name: "Test1Test2",
                newName: "JointTests");

            migrationBuilder.RenameIndex(
                name: "IX_Test1Test2_Tests2Id",
                table: "JointTests",
                newName: "IX_JointTests_Tests2Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JointTests",
                table: "JointTests",
                columns: new[] { "Tests1Id", "Tests2Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_JointTests_Test1_Tests1Id",
                table: "JointTests",
                column: "Tests1Id",
                principalTable: "Test1",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JointTests_Test2_Tests2Id",
                table: "JointTests",
                column: "Tests2Id",
                principalTable: "Test2",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JointTests_Test1_Tests1Id",
                table: "JointTests");

            migrationBuilder.DropForeignKey(
                name: "FK_JointTests_Test2_Tests2Id",
                table: "JointTests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JointTests",
                table: "JointTests");

            migrationBuilder.RenameTable(
                name: "JointTests",
                newName: "Test1Test2");

            migrationBuilder.RenameIndex(
                name: "IX_JointTests_Tests2Id",
                table: "Test1Test2",
                newName: "IX_Test1Test2_Tests2Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Test1Test2",
                table: "Test1Test2",
                columns: new[] { "Tests1Id", "Tests2Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_Test1Test2_Test1_Tests1Id",
                table: "Test1Test2",
                column: "Tests1Id",
                principalTable: "Test1",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Test1Test2_Test2_Tests2Id",
                table: "Test1Test2",
                column: "Tests2Id",
                principalTable: "Test2",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
