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
            services.AddOptions<TestOption>()
                .Configure(option =>
                {
                    option.ExampleString = configuration.GetValue<string>("TestOption:ExampleString");
                    option.ExampleBool = configuration.GetValue<bool>("TestOption:ExampleBool");
                    option.ExampleInt = configuration.GetValue<int>("TestOption:ExampleInt");
                })
                .Validate(x => x.Validate(), "Validação de ExampleString falhou");
        }
    }
}
