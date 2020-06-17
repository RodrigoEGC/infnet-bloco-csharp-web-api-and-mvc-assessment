using Domain.Model.Interfaces.Services;
using System.Net.Http;
using System.Threading.Tasks;

namespace Presentation.Mvc.HttpServices
{
    public interface IGroupHttpService: IGroupService
    {
        Task<HttpResponseMessage> GetByIdHttpAsync(int id);
    }
}
