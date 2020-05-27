using Data.Context.Configuration;
using Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options)
            :base(options)
        {

        }
        public DbSet<GroupEntity> Groups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GroupConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
