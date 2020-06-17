using Data.Context;
using Data.Repositories;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Interfaces.Services;
using Domain.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InversionOfControl
{
    public static class DependencyInjection
    {
        public static void RegisterInjections(
            this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            serviceCollection.AddDbContext<LibraryMusicalContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("LibraryMusicalContext")));

            serviceCollection.AddScoped<IAlbumService, AlbumService>();
            serviceCollection.AddScoped<IAlbumRepository, AlbumRepository>();
            serviceCollection.AddScoped<IGroupService, GroupService>();
            serviceCollection.AddScoped<IGroupRepository, GroupRepository>();
        }
        public static void RegisterDataAccess(
            this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            serviceCollection.AddDbContext<LibraryMusicalContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("LibraryMusicalContext")));

            serviceCollection.AddScoped<IAlbumRepository, AlbumRepository>();
        }
    }
}
