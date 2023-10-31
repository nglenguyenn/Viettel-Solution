using AutoMapper;
using AutoMapper.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using Viettel_Solution.DTO;
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
        [HttpGet]
        public async Task<IEnumerable<NewsDto>> GetNews()
        {
            var news = await _context.News.AsNoTracking().ToListAsync();
            foreach (var item in news)
            {
                item.Image = _storageService.GetFileUrl(item.Image);
            }
            var newsdto = _mapper.Map<IEnumerable<NewsDto>>(news);
            return newsdto;
        }
        //Get{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<NewsDto>> GetNewsById(string id)
        {
            var news = await _context.News.FindAsync(id);

            if (news == null)
            {
                return NotFound();
            }
            news.Image = _storageService.GetFileUrl(news.Image);
            var newsdto = _mapper.Map<NewsDto>(news);
            return newsdto;
        }
        //Create
        [HttpPost]
        public async Task<ActionResult<NewsDto>> PostNews([FromForm] NewsCreateRequest newsCreateRequest)
        {
            var news = _mapper.Map<News>(newsCreateRequest);
            news.Id = Guid.NewGuid().ToString();

            if (newsCreateRequest.ThumbnailImages != null)
            {
                news.Image = await SaveFile(newsCreateRequest.ThumbnailImages);
            }
            else
            {
                news.Image = "noimage.png";
            }
            news.Date = DateTime.Now;
            _context.News.Add(news);
            await _context.SaveChangesAsync();

            var newsdto = _mapper.Map<NewsDto>(news);
            return newsdto;
        }
        //Update
        [HttpPut("{id}")]
        public async Task<ActionResult<NewsDto>> PutNews(string id, [FromForm] NewsCreateRequest newsCreateRequest)
        {
            var news = await _context.News.FindAsync(id);

            if (news == null)
            {
                return NotFound();
            }

            if (newsCreateRequest.ThumbnailImages != null)
            {
                news.Image = await SaveFile(newsCreateRequest.ThumbnailImages);
            }

            _context.Entry<News>(news).CurrentValues.SetValues(newsCreateRequest);

            await _context.SaveChangesAsync();

            var newsdto = _mapper.Map<NewsDto>(news);

            return newsdto;
        }
        //Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult<NewsDto>> DeleteNews(string id)
        {
            var news = await _context.News.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            var newsdto = _mapper.Map<NewsDto>(news);
            _context.News.Remove(news);
            await _context.SaveChangesAsync();

            return newsdto;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}