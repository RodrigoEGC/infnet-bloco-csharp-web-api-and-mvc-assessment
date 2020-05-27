using Domain.Model.Entities;
using Domain.Model.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        public Task<bool> CheckNameAsync(string name, int id = -1)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GroupEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GroupEntity> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GroupEntity> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(GroupEntity insertedEntity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(GroupEntity updatedEntity)
        {
            throw new NotImplementedException();
        }
    }
}
