using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GAID.Domain.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProgramTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClosedReason",
                table: "Programs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsClosed",
                table: "Programs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClosedReason",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "IsClosed",
                table: "Programs");
        }
    }
}
