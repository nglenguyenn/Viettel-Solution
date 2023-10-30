using Microsoft.AspNetCore.Mvc;
using Viettel_Solution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViettelSolutions.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SolutionsController : ControllerBase
    {
        private readonly ViettelSolutionDbConext _context;

        public SolutionsController(ViettelSolutionDbConext context)
        {
            _context = context;
        }

        // GET: /solutions
        [HttpGet]
        public IActionResult Index()
        {
            // Lấy tất cả các giải pháp từ database
            var solutions = _context.Solutions.ToList();

            // Trả về danh sách các giải pháp
            return Ok(solutions);
        }

        // GET: /solutions/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // Tìm kiếm giải pháp có id được chỉ định
            var solution = _context.Solutions.Find(id);

            // Nếu giải pháp không tồn tại, trả về NotFound
            if (solution == null)
            {
                return NotFound();
            }
            // Trả về giải pháp
            return Ok(solution);

        }

        // POST: /solutions
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Solution solution)
        {
            // Thêm giải pháp mới vào database
            await _context.Solutions.AddAsync(solution);
            await _context.SaveChangesAsync();
            // Trả về giải pháp mới được tạo
            return CreatedAtAction("GetById", new { id = solution.Id }, solution);
        }

        // PUT: /solutions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Solution solution)
        {
            // Tìm kiếm giải pháp có id được chỉ định
            var existingSolution = await _context.Solutions.FindAsync(id);

            // Nếu giải pháp không tồn tại, trả về NotFound
            if (existingSolution == null)
            {
                return NotFound();
            }

            // Cập nhật giải pháp
            existingSolution.Name = solution.Name;
            existingSolution.Features = solution.Features;
            existingSolution.Description = solution.Description;
            existingSolution.Image = solution.Image;
            existingSolution.Category = solution.Category;

            await _context.SaveChangesAsync();

            // Trả về giải pháp đã được cập nhật
            return Ok(existingSolution);
        }

        // DELETE: /solutions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Tìm kiếm giải pháp có id được chỉ định
            var solution = await _context.Solutions.FindAsync(id);

            // Nếu giải pháp không tồn tại, trả về NotFound
            if (solution == null)
            {
                return NotFound();
            }

            // Xóa giải pháp
            _context.Solutions.Remove(solution);
            await _context.SaveChangesAsync();

            // Trả về NoContent
            return NoContent();
        }

    }
}
