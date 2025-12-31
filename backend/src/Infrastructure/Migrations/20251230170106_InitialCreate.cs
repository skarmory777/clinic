using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    full_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci"),
                    email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci"),
                    phone = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci"),
                    address = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8mb4_general_ci"),
                    city = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_general_ci"),
                    postcode = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, collation: "utf8mb4_general_ci"),
                    notes = table.Column<string>(type: "TEXT", nullable: false, collation: "utf8mb4_general_ci"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.id);
                })
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Username = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci"),
                    Email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci"),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci"),
                    FullName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_general_ci"),
                    ProfilePhoto = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_general_ci"),
                    Role = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    RefreshToken = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_general_ci"),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                })
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateIndex(
                name: "ix_customers_city",
                table: "customers",
                column: "city");

            migrationBuilder.CreateIndex(
                name: "ix_customers_email",
                table: "customers",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_customers_full_name",
                table: "customers",
                column: "full_name");

            migrationBuilder.CreateIndex(
                name: "ix_customers_phone",
                table: "customers",
                column: "phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_Email",
                table: "users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_Username",
                table: "users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
