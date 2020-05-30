using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Crosscutting.Identity
{
    public static class IdentityRegistration
    {
        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LoginContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("LoginContext")));
        }
        public static void RegisterIdentityForMvc(
            this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            AddDbContext(serviceCollection, configuration);
            serviceCollection.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<LoginContext>();
        }
        public static void RegisterIdentityForWebApi(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            AddDbContext(services, configuration);

            services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<LoginContext>();
        }
    }
}
