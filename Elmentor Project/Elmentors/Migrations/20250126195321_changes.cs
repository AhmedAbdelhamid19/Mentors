using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elmentors.Migrations
{
    /// <inheritdoc />
    public partial class changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TopicTopic_Topics_DependentTopicsId",
                table: "TopicTopic");

            migrationBuilder.RenameColumn(
                name: "DependentTopicsId",
                table: "TopicTopic",
                newName: "DependentId");

            migrationBuilder.RenameColumn(
                name: "TopicName",
                table: "Topics",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "TopicDescription",
                table: "Topics",
                newName: "Description");

            migrationBuilder.AddForeignKey(
                name: "FK_TopicTopic_Topics_DependentId",
                table: "TopicTopic",
                column: "DependentId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TopicTopic_Topics_DependentId",
                table: "TopicTopic");

            migrationBuilder.RenameColumn(
                name: "DependentId",
                table: "TopicTopic",
                newName: "DependentTopicsId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Topics",
                newName: "TopicName");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Topics",
                newName: "TopicDescription");

            migrationBuilder.AddForeignKey(
                name: "FK_TopicTopic_Topics_DependentTopicsId",
                table: "TopicTopic",
                column: "DependentTopicsId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
