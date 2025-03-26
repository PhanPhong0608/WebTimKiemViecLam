using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DACS_WebTimKiemViecLam.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionToField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Fields",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Fields");
        }
    }
}
