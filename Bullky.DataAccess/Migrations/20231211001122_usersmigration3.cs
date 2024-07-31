using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bulky.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class usersmigration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CreditCardNum",
                table: "users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Ssn",
                table: "users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreditCardNum", "Ssn" },
                values: new object[] { 7777777L, 30302080103752L });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreditCardNum", "Ssn" },
                values: new object[] { 7777777L, 30302080103752L });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreditCardNum",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Ssn",
                table: "users");
        }
    }
}
