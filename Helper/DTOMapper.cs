using AutoMapper;
using ProductsShop.DTO;
using ProductsShop.Models;

namespace ProductsShop.Helper
{
    public class DTOMapper : Profile
    {
        public static IMapper mapper { get; set; }
        static DTOMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductDTO, Product>().ForMember(O => O.DiscountRate, opt => opt.Ignore())
                .ReverseMap();
                cfg.CreateMap<UserDTO, User>().ForMember(O => O.UserId, opt => opt.Ignore())
                .ReverseMap();
            });
            configuration.AssertConfigurationIsValid();
            mapper = configuration.CreateMapper();
        }
    }
}
