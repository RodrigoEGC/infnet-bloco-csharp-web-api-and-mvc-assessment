using Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Context.Configuration
{
    public class AlbumConfiguration : IEntityTypeConfiguration<AlbumEntity> 
    {
        public void Configure(EntityTypeBuilder<AlbumEntity> builder)
        {

        }
    }
}
