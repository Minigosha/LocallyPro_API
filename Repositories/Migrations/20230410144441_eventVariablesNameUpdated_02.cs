using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class eventVariablesNameUpdated_02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Start",
                table: "Event",
                newName: "DateTimeStart");

            migrationBuilder.RenameColumn(
                name: "End",
                table: "Event",
                newName: "DateTimeEnd");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTimeStart",
                table: "Event",
                newName: "Start");

            migrationBuilder.RenameColumn(
                name: "DateTimeEnd",
                table: "Event",
                newName: "End");
        }
    }
}
