using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace Viettel_Solution.Models
{
    public class ViettelSolutionDbConext : DbContext
    {
        public ViettelSolutionDbConext(DbContextOptions<ViettelSolutionDbConext> options) : base(options)
        {     
        }

        // Khai báo các models
        public DbSet<Solution> Solutions { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<News> News { get; set; }
        // Các phương thức truy cập dữ liệu từ database
        public IQueryable<Solution> GetSolutions()
        {
            return this.Solutions;
        }
        public IQueryable<Feature> GetFeatures(int solutionId)
        {
            return this.Features.Where(f => f.SolutionId == solutionId);
        }

        public IQueryable<News> GetNews()
        {
            return this.News;
        }
    }
}
