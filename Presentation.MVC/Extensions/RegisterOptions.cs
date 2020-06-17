using Domain.Model.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.Mvc.Extensions
{
    public static class RegisterOptions
    {
        public static void RegisterOptionsConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<LibraryMusicalHttpOptions>(configuration.GetSection(nameof(LibraryMusicalHttpOptions)));
        }
    }
}
