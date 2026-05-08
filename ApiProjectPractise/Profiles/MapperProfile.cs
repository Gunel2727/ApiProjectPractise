using ApiProjectPractise.Dtos.CategoryDtos;
using ApiProjectPractise.Dtos.ProductDtos;
using ApiProjectPractise.Models;
using AutoMapper;

namespace ApiProjectPractise.Profiles
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        { 
           CreateMap<CategoryCreateDto,Category>();
            CreateMap<Category,CategoryReturnDto>();
            CreateMap<Product,ProductInCategoryReturnDto>();
            CreateMap<CategoryUpdateDto,Category>();
            CreateMap<ProductCreateDto,Product>();
                CreateMap<Product,ProductReturnDto>();
                CreateMap<Category,CategoryInProductReturnDto>();

        }
    }
}
