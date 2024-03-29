﻿using Data.Context;
using Domain.Model.Entities;
using Domain.Model.Exceptions;
using Domain.Model.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly LibraryMusicalContext _libraryContext;
        public GroupRepository(LibraryMusicalContext libraryContext)
        {
            _libraryContext = libraryContext;
        }
        public async Task<bool> CheckNameAsync(string name, int id)
        {
            var nameExists = await _libraryContext.Groups.AnyAsync(x => x.Name == name && x.Id != id);
            return nameExists;
        }

        public async Task DeleteAsync(int id)
        {
            var groupEntity = await _libraryContext.Groups.FindAsync(id);
            _libraryContext.Groups.Remove(groupEntity);
            await _libraryContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<GroupEntity>> GetAllAsync()
        {
            return await _libraryContext.Groups.ToListAsync();
        }

        public async Task<GroupEntity> GetByIdAsync(int id)
        {
            return await _libraryContext.Groups
                .Include(x => x.Albums)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<GroupEntity> GetByNameAsync(string name)
        {
            return await _libraryContext.Groups.SingleOrDefaultAsync(x => x.Name == name);
        }

        public async Task InsertAsync(GroupEntity insertedEntity)
        {
            _libraryContext.Add(insertedEntity);
            await _libraryContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(GroupEntity updatedEntity)
        {
            try
            {
                _libraryContext.Update(updatedEntity);
                await _libraryContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await GetByIdAsync(updatedEntity.Id) == null)
                {
                    throw new RepositoryException("Group not found");
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
