using Application.DTOs.Product;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            // Entity -> DTO (LEITURA)
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description ?? string.Empty))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category ?? string.Empty))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes ?? string.Empty))
                .AfterMap((src, dest) =>
                {
                    dest.PriceWithTax = src.CalculatePriceWithTax();
                    dest.TaxAmount = src.CalculateTax();
                    dest.ProfitMargin = src.CalculateProfitMargin();
                    dest.IsLowStock = src.IsLowStock();
                    dest.IsOutOfStock = src.IsOutOfStock();
                });

            // DTO -> Entity (ESCRITA)
            CreateMap<ProductDto, Product>()
                .ConstructUsing(dto => new Product(
                    dto.Sku,
                    dto.Name,
                    dto.UnitPrice,
                    dto.StockQuantity,
                    dto.Category,
                    dto.Description,
                    dto.CostPrice,
                    dto.TaxRate,
                    dto.UnitOfMeasure,
                    dto.MinStockLevel,
                    dto.ReorderQuantity,
                    dto.Notes
                ));
        }
    }
}
