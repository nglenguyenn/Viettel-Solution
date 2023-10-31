using AutoMapper;
using Viettel_Solution.DTO;
using Viettel_Solution.Models;

namespace Viettel_Solution.Mappings
{
    public class FeatureMapper : Profile
    {
        public FeatureMapper()
        {
            CreateMap<FeatureDto, Feature>().ReverseMap();
            CreateMap<FeatureCreateRequest, Feature>().ReverseMap();
        }
    }
}
