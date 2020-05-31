using Domain.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Model.Interfaces.Services
{
    public interface IGroupService
    {
        Task<IEnumerable<GroupEntity>> GetAllAsync();
        Task<GroupEntity> GetByIdAsync(int id);
        Task InsertAsync(GroupEntity insertedEntity);
        Task UpdateAsync(GroupEntity updatedEntity);
        Task DeleteAsync(int id);
        Task<bool> CheckNameAsync(string name, int id);
    }
}
