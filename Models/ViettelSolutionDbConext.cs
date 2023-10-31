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
    }
}
