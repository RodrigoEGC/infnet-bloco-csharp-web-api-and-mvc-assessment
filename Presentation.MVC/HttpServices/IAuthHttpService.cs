using Crosscutting.Identity.RequestModels;
using System.Threading.Tasks;

namespace Presentation.Mvc.HttpServices
{
    public interface IAuthHttpService
    {
        Task<string> GetTokenAsync(LoginRequest loginRequest);
    }
}
