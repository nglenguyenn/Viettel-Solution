using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Viettel_Solution.Migrations
{
    /// <inheritdoc />
    public partial class UpdateData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Solutions");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Features",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Features");

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Solutions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
