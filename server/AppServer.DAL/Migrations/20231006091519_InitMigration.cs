using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppServer.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HeavyTasks",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    description = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    result = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    startedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    finishedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    percentageDone = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeavyTasks", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HeavyTasks");
        }
    }
}
