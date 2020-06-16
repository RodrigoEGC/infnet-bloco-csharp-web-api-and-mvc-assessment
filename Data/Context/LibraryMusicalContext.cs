using Data.Context.Configuration;
using Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class LibraryMusicalContext : DbContext
    {
        public LibraryMusicalContext(DbContextOptions<LibraryMusicalContext> options)
            :base(options)
        {

        }
        public DbSet<GroupEntity> Groups { get; set; }
        public DbSet<AlbumEntity> Albums { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GroupConfiguration());
            modelBuilder.ApplyConfiguration(new AlbumConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
