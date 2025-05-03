using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElMentors.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Topic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topic", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TopicTopic",
                columns: table => new
                {
                    DependentsId = table.Column<int>(type: "int", nullable: false),
                    PrerequisitesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicTopic", x => new { x.DependentsId, x.PrerequisitesId });
                    table.ForeignKey(
                        name: "FK_TopicTopic_Topic_DependentsId",
                        column: x => x.DependentsId,
                        principalTable: "Topic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TopicTopic_Topic_PrerequisitesId",
                        column: x => x.PrerequisitesId,
                        principalTable: "Topic",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TopicTopic_PrerequisitesId",
                table: "TopicTopic",
                column: "PrerequisitesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TopicTopic");

            migrationBuilder.DropTable(
                name: "Topic");
        }
    }
}
