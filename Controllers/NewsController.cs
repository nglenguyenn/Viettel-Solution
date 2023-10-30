using Microsoft.AspNetCore.Mvc;
using Viettel_Solution.Models;


namespace ViettelSolutions.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly ViettelSolutionDbConext _context;

        public NewsController(ViettelSolutionDbConext context)
        {
            _context = context;
        }

        // GET: /news
        [HttpGet]
        public IActionResult Index()
        {
            // Lấy tất cả các tin tức từ database
            var news = _context.News.ToList();

            // Trả về danh sách các tin tức
            return Ok(news);
        }

        // GET: /news/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // Tìm kiếm tin tức có id được chỉ định
            var news = _context.News.Find(id);

            // Nếu tin tức không tồn tại, trả về NotFound
            if (news == null)
            {
                return NotFound();
            }

            // Trả về tin tức
            return Ok(news);
        }

        // POST: /news
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] News news)
        {
            // Thêm tin tức mới vào database
            await _context.News.AddAsync(news);
            await _context.SaveChangesAsync();

            // Trả về tin tức mới được tạo
            return CreatedAtAction("GetById", new { id = news.Id }, news);
        }

        // PUT: /news/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] News news)
        {
            // Tìm kiếm tin tức có id được chỉ định
            var existingNews = await _context.News.FindAsync(id);

            // Nếu tin tức không tồn tại, trả về NotFound
            if (existingNews == null)
            {
                return NotFound();
            }

            // Cập nhật tin tức
            existingNews.Title = news.Title;
            existingNews.Description = news.Description;
            existingNews.Image = news.Image;
            existingNews.Content = news.Content;
            existingNews.Category = news.Category;
            existingNews.Date = news.Date;

            await _context.SaveChangesAsync();

            // Trả về tin tức đã được cập nhật
            return Ok(existingNews);
        }

        // DELETE: /news/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Tìm kiếm tin tức có id được chỉ định
            var news = await _context.News.FindAsync(id);

            // Nếu tin tức không tồn tại, trả về NotFound
            if (news == null)
            {
                return NotFound();
            }

            // Xóa tin tức
            _context.News.Remove(news);
            await _context.SaveChangesAsync();

            // Trả về NoContent
            return NoContent();
        }
    }
}