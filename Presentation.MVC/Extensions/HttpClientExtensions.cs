using Domain.Model.Interfaces.Services;
using Domain.Model.Options;
using Domain.Service.Services;
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
            var libraryHttpOptionsSection = configuration.GetSection(nameof(LibraryMusicalHttpOptions));
            var libraryHttpOptions = libraryHttpOptionsSection.Get<LibraryMusicalHttpOptions>();

            services.AddHttpClient(libraryHttpOptions.Name, x => { x.BaseAddress = libraryHttpOptions.ApiBaseUrl; });

            services.AddScoped<IAlbumService, AlbumHttpService>();
            services.AddScoped<IGroupHttpService, GroupHttpService>();
            services.AddScoped<IAuthHttpService, AuthHttpService>();
        }
    }
}
