using Microsoft.AspNetCore.Mvc;
using Viettel_Solution.Models;

namespace ViettelSolutions.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeaturesController : ControllerBase
    {
        private readonly ViettelSolutionDbConext _context;

        public FeaturesController(ViettelSolutionDbConext context)
        {
            _context = context;
        }

        // GET: /features
        [HttpGet]
        public IActionResult Index()
        {
            // Lấy tất cả các tính năng từ database
            var features = _context.Features.ToList();

            // Trả về danh sách các tính năng
            return Ok(features);
        }

        // GET: /features/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // Tìm kiếm tính năng có id được chỉ định
            var feature = _context.Features.Find(id);

            // Nếu tính năng không tồn tại, trả về NotFound
            if (feature == null)
            {
                return NotFound();
            }

            // Trả về tính năng
            return Ok(feature);
        }

        // POST: /features
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Feature feature)
        {
            // Thêm tính năng mới vào database
            await _context.Features.AddAsync(feature);
            await _context.SaveChangesAsync();

            // Trả về tính năng mới được tạo
            return CreatedAtAction("GetById", new { id = feature.Id }, feature);
        }

        // PUT: /features/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Feature feature)
        {
            // Tìm kiếm tính năng có id được chỉ định
            var existingFeature = await _context.Features.FindAsync(id);

            // Nếu tính năng không tồn tại, trả về NotFound
            if (existingFeature == null)
            {
                return NotFound();
            }

            // Cập nhật tính năng
            existingFeature.Description = feature.Description;
            existingFeature.Name = feature.Name;

            await _context.SaveChangesAsync();

            // Trả về tính năng đã được cập nhật
            return Ok(existingFeature);
        }

        // DELETE: /features/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Tìm kiếm tính năng có id được chỉ định
            var feature = await _context.Features.FindAsync(id);

            // Nếu tính năng không tồn tại, trả về NotFound
            if (feature == null)
            {
                return NotFound();
            }

            // Xóa tính năng
            _context.Features.Remove(feature);
            await _context.SaveChangesAsync();

            // Trả về NoContent
            return NoContent();
        }
    }
}
