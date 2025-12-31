using System;

namespace Application.DTOs.Product
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Sku { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public decimal? CostPrice { get; set; }
        public decimal TaxRate { get; set; }
        public string UnitOfMeasure { get; set; } = string.Empty;
        public int StockQuantity { get; set; }
        public int MinStockLevel { get; set; }
        public int? ReorderQuantity { get; set; }
        public bool IsActive { get; set; }
        public string Notes { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        
        // Calculated fields
        public decimal PriceWithTax { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal? ProfitMargin { get; set; }
        public bool IsLowStock { get; set; }
        public bool IsOutOfStock { get; set; }
    }
}