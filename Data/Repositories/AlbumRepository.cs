using Domain.Model.Entities;
using Domain.Model.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class AlbumRepository : IAlbumRepository
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
