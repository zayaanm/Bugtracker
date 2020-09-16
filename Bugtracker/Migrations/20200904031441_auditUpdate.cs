using Microsoft.EntityFrameworkCore.Migrations;

namespace Bugtracker.Migrations
{
    public partial class auditUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketDescription",
                table: "AuditTickets");

            migrationBuilder.AddColumn<bool>(
                name: "DescriptionUpdated",
                table: "Tickets",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedEmail",
                table: "Tickets",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DescriptionUpdated",
                table: "AuditTickets",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedEmail",
                table: "AuditTickets",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionUpdated",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "UpdatedEmail",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "DescriptionUpdated",
                table: "AuditTickets");

            migrationBuilder.DropColumn(
                name: "UpdatedEmail",
                table: "AuditTickets");

            migrationBuilder.AddColumn<string>(
                name: "TicketDescription",
                table: "AuditTickets",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
