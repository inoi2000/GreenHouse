using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenHouse.Data.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddImagPath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Cities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Appartments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Appartments");
        }
    }
}
