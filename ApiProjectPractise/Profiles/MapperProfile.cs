using ApiProjectPractise.Dtos.CategoryDtos;
using ApiProjectPractise.Dtos.ProductDtos;
using ApiProjectPractise.Dtos.UserDtos;
using ApiProjectPractise.Extensions;
using ApiProjectPractise.Models;
using AutoMapper;

namespace ApiProjectPractise.Profiles
{
    public class MapperProfile:Profile
    {
        public MapperProfile(IHttpContextAccessor httpContextAccessor)
        { 
            var httpContext = httpContextAccessor.HttpContext;
            var uribuilder = new UriBuilder
            {
                Scheme = httpContext.Request.Scheme,
                Host = httpContext.Request.Host.Host,
                Port = httpContext.Request.Host.Port ?? 80
            };
            var url = uribuilder.Uri.AbsoluteUri;

            CreateMap<CategoryCreateDto,Category>()
                .ForMember(dest=>dest.ImageUrl, opt=>opt.MapFrom(src=>src.Photo.SaveFile("wwwroot/images")));

            CreateMap<Category, CategoryReturnDto>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src =>url + "images/" + src.ImageUrl));
               
            CreateMap<Product,ProductInCategoryReturnDto>();
            CreateMap<CategoryUpdateDto,Category>();
            CreateMap<ProductCreateDto,Product>();
                CreateMap<Product,ProductReturnDto>();
                CreateMap<Category,CategoryInProductReturnDto>();
            CreateMap<ProductUpdateDto,Product>();
            CreateMap<RegisterDto, AppUser>();

        }
    }
}
