using System;
using Domain.Common;
using Domain.Common.Interfaces;
using Domain.Enums;
using Domain.Events;

namespace Domain.Entities
{
    public class Product : BaseEntity, IAggregateRoot
    {
        public string Sku { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Category { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal? CostPrice { get; private set; }
        public decimal TaxRate { get; private set; }
        public string UnitOfMeasure { get; private set; }
        public int StockQuantity { get; private set; }
        public int MinStockLevel { get; private set; }
        public int? ReorderQuantity { get; private set; }
        public bool IsActive { get; private set; }
        public string? Notes { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        // Construtor privado para EF Core
        private Product() { }

        public Product(
            string sku,
            string name,
            decimal unitPrice,
            int stockQuantity,
            string category = null,
            string description = null,
            decimal? costPrice = null,
            decimal taxRate = 0.00m,
            string unitOfMeasure = "unit",
            int minStockLevel = 0,
            int? reorderQuantity = null,
            string notes = null)
        {
            Validate(sku, name, unitPrice, stockQuantity, taxRate, unitOfMeasure, minStockLevel);

            Id = Guid.NewGuid();
            Sku = sku;
            Name = name;
            Description = description;
            Category = category;
            UnitPrice = unitPrice;
            CostPrice = costPrice;
            TaxRate = taxRate;
            UnitOfMeasure = unitOfMeasure;
            StockQuantity = stockQuantity;
            MinStockLevel = minStockLevel;
            ReorderQuantity = reorderQuantity;
            IsActive = true;
            Notes = notes;
            CreatedAt = DateTime.UtcNow;

            AddDomainEvent(new ProductCreatedEvent(this));
        }

        private static void Validate(
            string sku,
            string name,
            decimal unitPrice,
            int stockQuantity,
            decimal taxRate,
            string unitOfMeasure,
            int minStockLevel,
            string category = null)
        {
            if (string.IsNullOrWhiteSpace(sku))
                throw new DomainException("SKU is required");
            
            if (sku.Length > 50)
                throw new DomainException("SKU cannot exceed 50 characters");

            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Name is required");
            
            if (name.Length > 200)
                throw new DomainException("Name cannot exceed 200 characters");

            if (unitPrice < 0)
                throw new DomainException("Unit price cannot be negative");

            if (stockQuantity < 0)
                throw new DomainException("Stock quantity cannot be negative");

            if (taxRate < 0 || taxRate > 100)
                throw new DomainException("Tax rate must be between 0 and 100");

            if (string.IsNullOrWhiteSpace(unitOfMeasure))
                throw new DomainException("Unit of measure is required");
            
            if (unitOfMeasure.Length > 20)
                throw new DomainException("Unit of measure cannot exceed 20 characters");

            if (minStockLevel < 0)
                throw new DomainException("Minimum stock level cannot be negative");

            if (category?.Length > 100)
                throw new DomainException("Category cannot exceed 100 characters");
        }

        public void Update(
            string name,
            string description,
            string category,
            decimal unitPrice,
            decimal? costPrice,
            decimal taxRate,
            string unitOfMeasure,
            int minStockLevel,
            int? reorderQuantity,
            string notes)
        {
            Validate(Sku, name, unitPrice, StockQuantity, taxRate, unitOfMeasure, minStockLevel);

            Name = name;
            Description = description;
            Category = category;
            UnitPrice = unitPrice;
            CostPrice = costPrice;
            TaxRate = taxRate;
            UnitOfMeasure = unitOfMeasure;
            MinStockLevel = minStockLevel;
            ReorderQuantity = reorderQuantity;
            Notes = notes;
            UpdatedAt = DateTime.UtcNow;

            AddDomainEvent(new ProductUpdatedEvent(this));
        }

        public void UpdateSku(string sku)
        {
            if (string.IsNullOrWhiteSpace(sku))
                throw new DomainException("SKU is required");
            
            if (sku.Length > 50)
                throw new DomainException("SKU cannot exceed 50 characters");

            Sku = sku;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateStock(int quantity)
        {
            if (quantity < 0)
                throw new DomainException("Stock quantity cannot be negative");

            StockQuantity = quantity;
            UpdatedAt = DateTime.UtcNow;

            AddDomainEvent(new ProductStockUpdatedEvent(this));
        }

        public void IncreaseStock(int quantity)
        {
            if (quantity <= 0)
                throw new DomainException("Quantity must be greater than zero");

            StockQuantity += quantity;
            UpdatedAt = DateTime.UtcNow;

            AddDomainEvent(new ProductStockUpdatedEvent(this));
        }

        public void DecreaseStock(int quantity)
        {
            if (quantity <= 0)
                throw new DomainException("Quantity must be greater than zero");

            if (StockQuantity - quantity < 0)
                throw new DomainException("Insufficient stock");

            StockQuantity -= quantity;
            UpdatedAt = DateTime.UtcNow;

            if (StockQuantity <= MinStockLevel)
            {
                AddDomainEvent(new ProductLowStockEvent(this));
            }

            AddDomainEvent(new ProductStockUpdatedEvent(this));
        }

        public void UpdatePrice(decimal unitPrice, decimal? costPrice = null)
        {
            if (unitPrice < 0)
                throw new DomainException("Unit price cannot be negative");

            if (costPrice < 0)
                throw new DomainException("Cost price cannot be negative");

            UnitPrice = unitPrice;
            CostPrice = costPrice;
            UpdatedAt = DateTime.UtcNow;

            AddDomainEvent(new ProductPriceUpdatedEvent(this));
        }

        public void Activate()
        {
            if (IsActive) return;

            IsActive = true;
            UpdatedAt = DateTime.UtcNow;

            AddDomainEvent(new ProductActivatedEvent(this));
        }

        public void Deactivate()
        {
            if (!IsActive) return;

            IsActive = false;
            UpdatedAt = DateTime.UtcNow;

            AddDomainEvent(new ProductDeactivatedEvent(this));
        }

        public bool IsLowStock() => StockQuantity <= MinStockLevel;
        
        public bool IsOutOfStock() => StockQuantity == 0;
        
        public decimal CalculateTax() => UnitPrice * (TaxRate / 100);
        
        public decimal CalculatePriceWithTax() => UnitPrice + CalculateTax();
        
        public decimal? CalculateProfitMargin()
        {
            if (!CostPrice.HasValue || CostPrice.Value == 0)
                return null;

            return ((UnitPrice - CostPrice.Value) / CostPrice.Value) * 100;
        }

        public bool CanSell(int quantity) => IsActive && StockQuantity >= quantity;
    }
}