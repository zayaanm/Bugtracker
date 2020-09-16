using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bugtracker.Migrations
{
    public partial class reaudit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditTickets",
                columns: table => new
                {
                    AuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketId = table.Column<int>(nullable: false),
                    TicketName = table.Column<string>(nullable: true),
                    TicketDescription = table.Column<string>(nullable: true),
                    TicketUpdated = table.Column<DateTime>(nullable: true),
                    TicketPriority = table.Column<string>(nullable: true),
                    TicketType = table.Column<string>(nullable: true),
                    TicketStatus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTickets", x => x.AuditId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditTickets");
        }
    }
}
