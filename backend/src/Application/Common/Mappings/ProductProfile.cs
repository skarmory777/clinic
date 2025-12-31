using Application.DTOs.Product;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
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
        }
    }
}