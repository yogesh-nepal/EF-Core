using AutoMapper;
using EFmodels;
using EFmodels.Resource;

namespace EFcoreAPI
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {

            //Resource to API Mapping
            CreateMap<MultipleImageDataResource, MultipleImageData>();
            CreateMap<MultipleImageData, MultipleImageDataResource>();
            CreateMap<MediaPostModel, MediaPostResource>();
            CreateMap<MediaPostResource, MediaPostModel>();


            //API to Resource

            CreateMap<MultipleImageData, MultipleImageDataResource>();
            CreateMap<APostWithMultipleImageModel, PostWithMultipleImageResource>()
            .ForMember(p => p.MultipleImageData, m => m.MapFrom(s => s.MultipleImageData));


        }
    }
}