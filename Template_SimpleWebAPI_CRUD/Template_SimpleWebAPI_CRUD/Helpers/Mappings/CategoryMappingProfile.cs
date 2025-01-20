using AutoMapper;
using DTO.Categories;
using Template_SimpleWebAPI_CRUD.Models;
using Template_SimpleWebAPI_CRUD.Models.DTO.Categories;

namespace Template_SimpleWebAPI_CRUD.Helpers.Mappings
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<CategoryAddRequestDto, Category>()
                .ReverseMap();

            CreateMap<Category, CategoryResponseDto>()
                .ReverseMap()
                .ForAllMembers(x => x.Condition(
                    (src, dest, property) =>
                    {
                        //Пропускаем null и пустые строки
                        if (property == null) 
                            return false;

                        if (property.GetType() == typeof(string) && string.IsNullOrEmpty((string)property)) return false;

                        return true;
                    }));

            CreateMap<CategoryUpdateRequestDto, Category>().ReverseMap();
        }
    }
}
