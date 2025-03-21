using AutoMapper;
using CustomerOnboarding.Domain.DataTransferObjects;
using CustomerOnboarding.Domain.Entities;

namespace CustomerOnboarding.Services.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerDto, Customer>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()); // Ignoring PasswordHash here since it'll be hashed separately  

            CreateMap<Customer, CustomerDto>();
        }
    }
}