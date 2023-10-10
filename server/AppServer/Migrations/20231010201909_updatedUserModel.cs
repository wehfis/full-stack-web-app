using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppServer.Migrations
{
    /// <inheritdoc />
    public partial class updatedUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "Users",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "hashedPassword",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "startedAt",
                table: "HeavyTasks",
                newName: "StartedAt");

            migrationBuilder.RenameColumn(
                name: "result",
                table: "HeavyTasks",
                newName: "Result");

            migrationBuilder.RenameColumn(
                name: "percentageDone",
                table: "HeavyTasks",
                newName: "PercentageDone");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "HeavyTasks",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "finishedAt",
                table: "HeavyTasks",
                newName: "FinishedAt");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "HeavyTasks",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "HeavyTasks",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "hashedPassword");

            migrationBuilder.RenameColumn(
                name: "StartedAt",
                table: "HeavyTasks",
                newName: "startedAt");

            migrationBuilder.RenameColumn(
                name: "Result",
                table: "HeavyTasks",
                newName: "result");

            migrationBuilder.RenameColumn(
                name: "PercentageDone",
                table: "HeavyTasks",
                newName: "percentageDone");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "HeavyTasks",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "FinishedAt",
                table: "HeavyTasks",
                newName: "finishedAt");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "HeavyTasks",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "HeavyTasks",
                newName: "id");
        }
    }
}
