namespace CleanMinimalApi.Infrastructure;

using System;
using CleanMinimalApi.Application.Authentications;
using CleanMinimalApi.Application.Destinations;
using CleanMinimalApi.Infrastructure.Databases.Tourism;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{

    //Dependency Injection
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Add DbContext
        /*_ = services.AddDbContext<MovieReviewsDbContext>(options =>
            options.UseInMemoryDatabase($"Movies-{Guid.NewGuid()}"), ServiceLifetime.Singleton);*/
        /*   _ = services.AddDbContext<TourismDbContext>(options =>
               options.UseSqlServer(GetConnectionString())
           );
   */
         _ = services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


        //Add Service
       /* _ = services.AddSingleton<EntityFrameworkMovieReviewsRepository>();

        _ = services.AddSingleton<IAuthorsRepository>(p =>
            p.GetRequiredService<EntityFrameworkMovieReviewsRepository>());
        _ = services.AddSingleton<IMoviesRepository>(x =>
            x.GetRequiredService<EntityFrameworkMovieReviewsRepository>());
        _ = services.AddSingleton<IReviewsRepository>(x =>
            x.GetRequiredService<EntityFrameworkMovieReviewsRepository>());*/

        _ = services.AddSingleton<EntityFrameworkTourismRepository>();
    /*    _ = services.AddScoped<IDestinationsRepository>(p =>
            p.GetRequiredService<EntityFrameworkTourismRepository>());*/

        _ = services.AddScoped<IAuthenticationRepository>(p =>
          p.GetRequiredService<EntityFrameworkTourismRepository>());

        _ = services.AddSingleton(TimeProvider.System);

        return services;
    }

}
