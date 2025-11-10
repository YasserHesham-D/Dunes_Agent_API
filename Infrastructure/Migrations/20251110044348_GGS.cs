using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class GGS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Room",
                table: "ReciptVouchers",
                newName: "NumberOfRooms");

            migrationBuilder.AddColumn<string>(
                name: "AgentName",
                table: "ReciptVouchers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgentName",
                table: "ReciptVouchers");

            migrationBuilder.RenameColumn(
                name: "NumberOfRooms",
                table: "ReciptVouchers",
                newName: "Room");
        }
    }
}
