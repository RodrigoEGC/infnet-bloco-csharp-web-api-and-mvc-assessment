using Domain.Model.Entities;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Service.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;

        public GroupService(
            IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }
        public Task<bool> CheckNameAsync(string name, int id)
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
