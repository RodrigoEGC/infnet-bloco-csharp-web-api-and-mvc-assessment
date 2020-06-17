using Data.Context;
using Domain.Model.Entities;
using Domain.Model.Exceptions;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly LibraryMusicalContext _context;
        private readonly IOptionsMonitor<TestOption> _testOption;

        public AlbumRepository(
            LibraryMusicalContext libraryMusicalContext,
            IOptionsMonitor<TestOption> testOption)
        {
            _context = libraryMusicalContext;
            _testOption = testOption;
        }
        public async Task DeleteAsync(int id)
        {
            var albumModel = await _context.Albums.FindAsync(id);
            _context.Albums.Remove(albumModel);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AlbumEntity>> GetAllAsync()
        {
            return await _context.Albums.ToListAsync();
        }

        public async Task<AlbumEntity> GetByIdAsync(int id)
        {
            return await _context.Albums.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task InsertAsync(AlbumEntity insertedEntity)
        {
            _context.Add(insertedEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AlbumEntity updatedEntity)
        {
            try
            {
                _context.Update(updatedEntity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await GetByIdAsync(updatedEntity.Id) == null)
                {
                    throw new RepositoryException("Album not found!");
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
