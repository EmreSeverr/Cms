using Cms.Api.Cache.Abstract;
using Cms.Api.Cache.Concrate;
using Cms.Api.DTO;
using Cms.Api.Services.Abstract;
using Cms.Api.Services.Concrate;
using Cms.Data;
using Cms.Data.Repositories.Abstract;
using Cms.Data.Repositories.Concrate;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Reflection;

namespace Cms.Api
{
    public static class StartupConfiguration
    {
        /// <summary>
        /// Applies dependency injection to services.
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static void ConfigureDependencyInjections(this IServiceCollection serviceCollection)
        {
            #region Repositories

            serviceCollection.AddScoped(typeof(ICmsBaseRepository<>), typeof(CmsBaseRepository<>));
            serviceCollection.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            serviceCollection.AddScoped(typeof(IContentRepository), typeof(ContentRepository));

            #endregion

            #region Services

            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<IContentService, ContentService>();
            serviceCollection.AddScoped<ICacheService, CacheService>();

            #endregion

            serviceCollection.AddHttpContextAccessor();
        }

        /// <summary>
        /// Migration database connection clause.
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static void ConfigureDbContext(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<CmsDbContext>(p =>
            {
                p.UseNpgsql(builder.Configuration.GetConnectionString("CmsDb"), p => p.MigrationsAssembly("Cms.Api"));
            });
        }
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(opt =>
            {
                opt.AddPolicy("CmsCors", builder => builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());
            });
        }

        public static void ConfigureRedis(this WebApplicationBuilder builder)
        {
            var redConString = builder.Configuration.GetConnectionString("Redis");

            var redisConnection = ConnectionMultiplexer.Connect(redConString);

            builder.Services.AddSingleton<IConnectionMultiplexer>(redisConnection);
        }

        public static void AddMapster(this IServiceCollection services)
        {
            var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
            Assembly applicationAssembly = typeof(BaseDto).Assembly;
            typeAdapterConfig.Scan(applicationAssembly);

            var mapperConfig = new Mapper(typeAdapterConfig);
            services.AddSingleton<IMapper>(mapperConfig);
        }

        public static async Task MigrateAsync(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

            using var context = serviceScope.ServiceProvider.GetService<CmsDbContext>();

            try
            {
                await context.Database.MigrateAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new Exception("Database migrate exception.");
            }
        }
    }
}
