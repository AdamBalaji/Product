using AutoMapper;
using Product.DTO.Category;
using Product.DTO.SubCategory;
using Product.Model;

namespace Product.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCategoryDto,Category>();
            CreateMap<UpdateCategoryDto,Category>();
            CreateMap<Category,CategoryDto>();

            CreateMap<CreateSubCategoryDto, SubCategory>();
            CreateMap<UpdateSubCategoryDto, SubCategory>();
            CreateMap<SubCategory, SubCategoryDto>();
        }
    }
}
