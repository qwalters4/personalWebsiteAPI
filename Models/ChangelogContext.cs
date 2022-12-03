using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace RDPDocumentationWebAPI.Models
{
    public class ChangelogContext : DbContext
    {
        public ChangelogContext(DbContextOptions<ChangelogContext> options) : base(options)
        {
        }
        public DbSet<Changelog> ChangelogItems { get; set; }
    }
}
