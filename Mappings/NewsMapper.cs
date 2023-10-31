using AutoMapper;
using Viettel_Solution.DTO;
using Viettel_Solution.Models;

namespace Viettel_Solution.Mappings
{
    public class NewsMapper : Profile
    {
        public NewsMapper()
        {
            CreateMap<NewsDto, News>().ReverseMap();
            CreateMap<NewsCreateRequest, News>().ReverseMap();
        }
    }
}
