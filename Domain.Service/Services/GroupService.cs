using Domain.Model.Entities;
using Domain.Model.Exceptions;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Interfaces.Services;
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
        public async Task<bool> CheckNameAsync(string name, int id)
        {
            return await _groupRepository.CheckNameAsync(name, id);
        }

        public async Task DeleteAsync(int id)
        {
            await _groupRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<GroupEntity>> GetAllAsync()
        {
            return await _groupRepository.GetAllAsync();
        }

        public async Task<GroupEntity> GetByIdAsync(int id)
        {
            return await _groupRepository.GetByIdAsync(id);
        }

        public async Task InsertAsync(GroupEntity insertedEntity)
        {
            var nameExists = await _groupRepository.CheckNameAsync(insertedEntity.Name);
            if (nameExists)
            {
                throw new EntityValidationException(nameof(GroupEntity.Name), $"Name: {insertedEntity.Name } já existe!");
            }
            await _groupRepository.InsertAsync(insertedEntity);
        }

        public async Task UpdateAsync(GroupEntity updatedEntity)
        {
            var nameExists = await _groupRepository.CheckNameAsync(updatedEntity.Name, updatedEntity.Id);
            if (nameExists)
            {
                throw new EntityValidationException(nameof(GroupEntity.Name), $"Name: {updatedEntity.Name} já existe!");
            }
            await _groupRepository.UpdateAsync(updatedEntity);
        }
    }
}
