using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class altertablecustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "postcode",
                table: "customers",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20)
                .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "notes",
                table: "customers",
                type: "TEXT",
                nullable: true,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "TEXT")
                .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "address",
                table: "customers",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldMaxLength: 500)
                .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "customers",
                keyColumn: "postcode",
                keyValue: null,
                column: "postcode",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "postcode",
                table: "customers",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.UpdateData(
                table: "customers",
                keyColumn: "notes",
                keyValue: null,
                column: "notes",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "notes",
                table: "customers",
                type: "TEXT",
                nullable: false,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.UpdateData(
                table: "customers",
                keyColumn: "address",
                keyValue: null,
                column: "address",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "address",
                table: "customers",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldMaxLength: 500,
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");
        }
    }
}
