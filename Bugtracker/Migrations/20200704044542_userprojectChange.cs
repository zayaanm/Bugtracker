using Microsoft.EntityFrameworkCore.Migrations;

namespace Bugtracker.Migrations
{
    public partial class userprojectChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_ProjectCreatorId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ProjectCreatorId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectCreatorId",
                table: "Projects");

            migrationBuilder.AddColumn<string>(
                name: "UserRole",
                table: "UserProjects",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserRole",
                table: "UserProjects");

            migrationBuilder.AddColumn<string>(
                name: "ProjectCreatorId",
                table: "Projects",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectCreatorId",
                table: "Projects",
                column: "ProjectCreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_ProjectCreatorId",
                table: "Projects",
                column: "ProjectCreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
