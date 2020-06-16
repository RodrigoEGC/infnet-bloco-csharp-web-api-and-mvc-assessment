using Domain.Model.Entities;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;

        public AlbumService(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        } 
        public async Task DeleteAsync(int id)
        {
            await _albumRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<AlbumEntity>> GetAllAsync()
        {
            return await _albumRepository.GetAllAsync();
        }

        public async Task<AlbumEntity> GetByIdAsync(int id)
        {
            return await _albumRepository.GetByIdAsync(id);
        }

        public async Task InsertAsync(AlbumEntity insertedEntity)
        {
            await _albumRepository.InsertAsync(insertedEntity);
        }

        public async Task UpdateAsync(AlbumEntity updatedEntity)
        {
            await _albumRepository.UpdateAsync(updatedEntity);
        }
    }
}
