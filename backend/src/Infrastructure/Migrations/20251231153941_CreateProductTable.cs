using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    sku = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci"),
                    name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, collation: "utf8mb4_general_ci"),
                    description = table.Column<string>(type: "TEXT", nullable: false, collation: "utf8mb4_general_ci"),
                    category = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_general_ci"),
                    unit_price = table.Column<decimal>(type: "DECIMAL(10,2)", nullable: false),
                    cost_price = table.Column<decimal>(type: "DECIMAL(10,2)", nullable: true),
                    tax_rate = table.Column<decimal>(type: "DECIMAL(5,2)", nullable: false, defaultValue: 0.00m),
                    unit_of_measure = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, defaultValue: "unit", collation: "utf8mb4_general_ci"),
                    stock_quantity = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    min_stock_level = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    reorder_quantity = table.Column<int>(type: "int", nullable: true),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    notes = table.Column<string>(type: "TEXT", nullable: true, collation: "utf8mb4_general_ci"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.id);
                    table.CheckConstraint("CK_Product_MinStockLevel", "min_stock_level >= 0");
                    table.CheckConstraint("CK_Product_StockQuantity", "stock_quantity >= 0");
                    table.CheckConstraint("CK_Product_TaxRate", "tax_rate >= 0 AND tax_rate <= 100");
                    table.CheckConstraint("CK_Product_UnitPrice", "unit_price >= 0");
                })
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateIndex(
                name: "ix_products_category",
                table: "products",
                column: "category");

            migrationBuilder.CreateIndex(
                name: "ix_products_is_active",
                table: "products",
                column: "is_active");

            migrationBuilder.CreateIndex(
                name: "ix_products_name",
                table: "products",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "ix_products_sku",
                table: "products",
                column: "sku",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "products");
        }
    }
}
