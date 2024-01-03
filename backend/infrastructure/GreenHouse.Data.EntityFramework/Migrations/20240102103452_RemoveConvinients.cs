using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenHouse.Data.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class RemoveConvinients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Conveniences",
                table: "Appartments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Conveniences",
                table: "Appartments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
