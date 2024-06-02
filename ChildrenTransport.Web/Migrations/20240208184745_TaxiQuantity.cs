using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChildrenTransport.Web.Migrations
{
    /// <inheritdoc />
    public partial class TaxiQuantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Taxi",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Taxi");
        }
    }
}
