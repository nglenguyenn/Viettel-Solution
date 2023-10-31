using AutoMapper;
using Viettel_Solution.DTO;
using Viettel_Solution.Models;

namespace Viettel_Solution.Mappings
{
    public class SolutionMapper : Profile
    {
        public  SolutionMapper()
        {
            CreateMap<SolutionDto, Solution>().ReverseMap();
            CreateMap<SolutionCreateRequest, Solution>().ReverseMap();
        }
    }
}
