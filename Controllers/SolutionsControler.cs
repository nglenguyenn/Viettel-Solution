using Microsoft.AspNetCore.Mvc;
using Viettel_Solution.Models;
using Viettel_Solution.Storage;
using Microsoft.Net.Http.Headers;
using AutoMapper;
using Viettel_Solution.DTO;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;

namespace ViettelSolutions.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SolutionsController : ControllerBase
    {
        private readonly ViettelSolutionDbConext _context;
        private readonly IStorageService _storageService;
        private readonly IMapper _mapper;

        public SolutionsController(ViettelSolutionDbConext context,IMapper mapper,IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
            _mapper = mapper;
        }

        // GET: /solutions
        [HttpGet]
        public async Task<IEnumerable<SolutionDto>> GetSolution()
        {
            var solution = await _context.Solutions.ToListAsync();
            foreach (var item in solution) 
            { 
                item.Image = _storageService.GetFileUrl(item.Image);
            }
            var solutiondto = _mapper.Map<IEnumerable<SolutionDto>>(solution);
            return solutiondto;
        }
        //Get{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<SolutionDto>> GetSolution(string id)
        {
            var solution = await _context.Solutions.FindAsync(id);

            if (solution == null)
            {
                return NotFound();
            }
            solution.Image = _storageService.GetFileUrl(solution.Image);
            var solutiondto = _mapper.Map<SolutionDto>(solution);
            return solutiondto;
        }
        //Create
        [HttpPost]
        public async Task<ActionResult<SolutionDto>> PostSolution([FromForm] SolutionCreateRequest solutionCreateRequest)
        {
            var solution = _mapper.Map<Solution>(solutionCreateRequest);
            solution.Id = Guid.NewGuid().ToString();

            if (solutionCreateRequest.ThumbnailImages != null)
            {
                solution.Image = await SaveFile(solutionCreateRequest.ThumbnailImages);
            }
            else
            {
                solution.Image = "noimage.png";
            }

            _context.Solutions.Add(solution);
            await _context.SaveChangesAsync();

            var solutiondto = _mapper.Map<SolutionDto>(solution);
            return solutiondto;
        }
        //Update
        [HttpPut("{id}")]
        public async Task<ActionResult<SolutionDto>> PutSolution(string id, [FromForm] SolutionCreateRequest solutionCreateRequest)
        {
            var solution = await _context.Solutions.FindAsync(id);

            if (solution == null)
            {
                return NotFound();
            }

            if (solutionCreateRequest.ThumbnailImages != null)
            {
                solution.Image = await SaveFile(solutionCreateRequest.ThumbnailImages);
            }

            _context.Entry<Solution>(solution).CurrentValues.SetValues(solutionCreateRequest);

            await _context.SaveChangesAsync();

            var solutiondto = _mapper.Map<SolutionDto>(solution);

            return solutiondto;
        }
        //Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult<SolutionDto>> DeleteSolution(string id)
        {
            var solution = await _context.Solutions.FindAsync(id);
            if (solution == null)
            {
                return NotFound();
            }
            var solutiondto = _mapper.Map<SolutionDto>(solution);
            _context.Solutions.Remove(solution);
            await _context.SaveChangesAsync();

            return solutiondto;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Value.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}
