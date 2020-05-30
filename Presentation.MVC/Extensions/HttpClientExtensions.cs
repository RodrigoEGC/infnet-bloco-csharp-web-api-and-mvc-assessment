using Domain.Model.Interfaces.Services;
using Domain.Model.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Mvc.HttpServices;

namespace Presentation.Mvc.Extensions
{
    public static class HttpClientExtensions
    {
        public static void RegisterHttpClients(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var libraryHttpOptionsSection = configuration.GetSection(nameof(LibraryHttpOptions));
            var libraryHttpOptions = libraryHttpOptionsSection.Get<LibraryHttpOptions>();

            services.AddHttpClient(libraryHttpOptions.Name, x => { x.BaseAddress = libraryHttpOptions.ApiBaseUrl; });

            services.AddScoped<IGroupService, GroupHttpService>();
            services.AddScoped<IAuthHttpService, AuthHttpService>();
        }
    }
}
