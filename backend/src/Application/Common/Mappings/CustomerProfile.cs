using AutoMapper;
using Application.DTOs.Customer;
using Domain.Entities;

namespace Application.Mappings
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            // DTO -> Entity (Create / Update)
            CreateMap<CustomerDto, Customer>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            // Entity -> DTO (Get)
            CreateMap<Customer, CustomerDto>();
        }
    }
}
