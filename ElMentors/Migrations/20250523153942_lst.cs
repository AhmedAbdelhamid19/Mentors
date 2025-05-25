using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElMentors.Migrations
{
    /// <inheritdoc />
    public partial class lst : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Child_Test2_Test2FK",
                table: "Child");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Child",
                table: "Child");

            migrationBuilder.RenameTable(
                name: "Child",
                newName: "Test");

            migrationBuilder.RenameColumn(
                name: "zing",
                table: "Test",
                newName: "zong");

            migrationBuilder.RenameIndex(
                name: "IX_Child_Test2FK",
                table: "Test",
                newName: "IX_Test_Test2FK");

            migrationBuilder.AlterColumn<double>(
                name: "Speed",
                table: "Test",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Test",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Test",
                table: "Test",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Test_Test2_Test2FK",
                table: "Test",
                column: "Test2FK",
                principalTable: "Test2",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Test_Test2_Test2FK",
                table: "Test");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Test",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Test");

            migrationBuilder.RenameTable(
                name: "Test",
                newName: "Child");

            migrationBuilder.RenameColumn(
                name: "zong",
                table: "Child",
                newName: "zing");

            migrationBuilder.RenameIndex(
                name: "IX_Test_Test2FK",
                table: "Child",
                newName: "IX_Child_Test2FK");

            migrationBuilder.AlterColumn<double>(
                name: "Speed",
                table: "Child",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Child",
                table: "Child",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Child_Test2_Test2FK",
                table: "Child",
                column: "Test2FK",
                principalTable: "Test2",
                principalColumn: "Id");
        }
    }
}
