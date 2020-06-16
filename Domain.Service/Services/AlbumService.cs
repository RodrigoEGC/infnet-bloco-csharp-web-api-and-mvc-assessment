using Domain.Model.Entities;
using Domain.Model.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Services
{
    public class AlbumService : IAlbumService
    {
        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AlbumEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AlbumEntity> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(AlbumEntity insertedEntity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(AlbumEntity updatedEntity)
        {
            throw new NotImplementedException();
        }
    }
}
