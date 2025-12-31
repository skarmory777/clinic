using System;
using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public abstract class ProductEvent : IDomainEvent
    {
        public Guid ProductId { get; }
        public string ProductName { get; }
        public DateTime OccurredOn { get; }

        protected ProductEvent(Product product)
        {
            ProductId = product.Id;
            ProductName = product.Name;
            OccurredOn = DateTime.UtcNow;
        }
    }

    public class ProductCreatedEvent : ProductEvent
    {
        public ProductCreatedEvent(Product product) : base(product)
        {
        }
    }

    public class ProductUpdatedEvent : ProductEvent
    {
        public ProductUpdatedEvent(Product product) : base(product)
        {
        }
    }

    public class ProductStockUpdatedEvent : ProductEvent
    {
        public int StockQuantity { get; }
        public int MinStockLevel { get; }

        public ProductStockUpdatedEvent(Product product) : base(product)
        {
            StockQuantity = product.StockQuantity;
            MinStockLevel = product.MinStockLevel;
        }
    }

    public class ProductLowStockEvent : ProductEvent
    {
        public int StockQuantity { get; }
        public int MinStockLevel { get; }

        public ProductLowStockEvent(Product product) : base(product)
        {
            StockQuantity = product.StockQuantity;
            MinStockLevel = product.MinStockLevel;
        }
    }

    public class ProductPriceUpdatedEvent : ProductEvent
    {
        public decimal NewPrice { get; }
        public decimal? OldPrice { get; }

        public ProductPriceUpdatedEvent(Product product, decimal? oldPrice = null) : base(product)
        {
            NewPrice = product.UnitPrice;
            OldPrice = oldPrice;
        }
    }

    public class ProductActivatedEvent : ProductEvent
    {
        public ProductActivatedEvent(Product product) : base(product)
        {
        }
    }

    public class ProductDeactivatedEvent : ProductEvent
    {
        public ProductDeactivatedEvent(Product product) : base(product)
        {
        }
    }
}