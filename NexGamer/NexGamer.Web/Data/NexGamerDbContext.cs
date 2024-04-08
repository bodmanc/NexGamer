using Microsoft.EntityFrameworkCore;
using NexGamer.Web.Models.Domain;
using System.Security.Principal;

namespace NexGamer.Web.Data
{
    public class NexGamerDbContext : DbContext
    {
        public NexGamerDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
