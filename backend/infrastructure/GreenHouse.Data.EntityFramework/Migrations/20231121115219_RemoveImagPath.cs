using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenHouse.Data.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class RemoveImagPath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Appartments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Appartments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
