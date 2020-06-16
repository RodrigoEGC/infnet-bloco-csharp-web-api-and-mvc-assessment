using Domain.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Model.Interfaces.Services
{
    public interface IAlbumService
    {
        Task<IEnumerable<AlbumEntity>> GetAllAsync();
        Task<AlbumEntity> GetByIdAsync(int id);
        Task InsertAsync(AlbumEntity insertedEntity);
        Task UpdateAsync(AlbumEntity updatedEntity);
        Task DeleteAsync(int id);
    }
}
