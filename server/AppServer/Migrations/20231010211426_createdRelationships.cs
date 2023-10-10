using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppServer.Migrations
{
    /// <inheritdoc />
    public partial class createdRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "HeavyTasks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_HeavyTasks_OwnerId",
                table: "HeavyTasks",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_HeavyTasks_Users_OwnerId",
                table: "HeavyTasks",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HeavyTasks_Users_OwnerId",
                table: "HeavyTasks");

            migrationBuilder.DropIndex(
                name: "IX_HeavyTasks_OwnerId",
                table: "HeavyTasks");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "HeavyTasks");
        }
    }
}
