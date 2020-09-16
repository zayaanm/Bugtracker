using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bugtracker.Migrations
{
    public partial class audit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Audit_Tickets",
                columns: table => new
                {
                    TicketId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketName = table.Column<string>(nullable: true),
                    TicketDescription = table.Column<string>(nullable: true),
                    TicketUpdated = table.Column<DateTime>(nullable: true),
                    TicketPriority = table.Column<string>(nullable: true),
                    TicketType = table.Column<string>(nullable: true),
                    TicketStatus = table.Column<string>(nullable: false),
                    SubmitterEmail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audit_Tickets", x => x.TicketId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Audit_Tickets");
        }
    }
}
