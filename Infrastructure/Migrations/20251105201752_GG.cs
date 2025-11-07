using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class GG : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Services",
                newName: "ServiceName");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Opreations",
                newName: "EntryDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ServiceName",
                table: "Services",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "EntryDate",
                table: "Opreations",
                newName: "Date");
        }
    }
}
