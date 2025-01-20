using AutoMapper;
using Template_SimpleWebAPI_CRUD.Models;
using Template_SimpleWebAPI_CRUD.Models.DTO.Products;

namespace Template_SimpleWebAPI_CRUD.Helpers.Mappings
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<ProductCreateRequestDto, Product>().ReverseMap();

            CreateMap<Product, ProductResponseDto>().ReverseMap()
                .ForAllMembers(x => x.Condition(
                    (src, dest, property) =>
                    {
                        // Пропускаем null и пустые строки
                        if (property == null) return false;
                        if (property.GetType() == typeof(string) && string.IsNullOrEmpty((string)property)) return false;

                        return true;
                    }));

            CreateMap<ProductUpdateRequestDto, Product>().ReverseMap();
        }


    }
}
