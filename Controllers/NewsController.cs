using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Viettel_Solution.Models;
using Viettel_Solution.Storage;

namespace ViettelSolutions.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly ViettelSolutionDbConext _context;
        private readonly IStorageService _storageService;
        private readonly IMapper _mapper;

        public NewsController(ViettelSolutionDbConext context, IMapper mapper, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
            _mapper = mapper;
        }
    }
}