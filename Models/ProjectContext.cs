using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace PersonalWebsiteWebAPI.Models
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {
        }
        public DbSet<ProjectModel> Projects { get; set; }
    }
}
