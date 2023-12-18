using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop_API.Migrations
{
    /// <inheritdoc />
    public partial class InitDbV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Discount",
                table: "Bill",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPayment",
                table: "Bill",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Payment",
                table: "Bill",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Bill");

            migrationBuilder.DropColumn(
                name: "IsPayment",
                table: "Bill");

            migrationBuilder.DropColumn(
                name: "Payment",
                table: "Bill");
        }
    }
}
