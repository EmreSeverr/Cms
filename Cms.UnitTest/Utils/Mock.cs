using Cms.Api;
using Cms.Data;
using Cms.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Cms.UnitTest.Utils
{
    public class Mock
    {
        private static bool _isProductionTest = false;

        public async Task ResetAllDatasAsync()
        {
            await ResetUsersAsync().ConfigureAwait(false);
            await ResetContentAsync().ConfigureAwait(false);
        }

        private async Task ResetDatasAsync<TEntity>(List<TEntity> entities) where TEntity : class
        {
            IConfiguration configuration = GetConfiguration();

            var connectionString = configuration.GetConnectionString("CmsTestDb");

            var options = new DbContextOptionsBuilder<CmsDbContext>().UseNpgsql(connectionString, o => { o.MigrationsAssembly("Cms.Api"); }).Options;

            using var context = new CmsDbContext(options);

            await context.Database.MigrateAsync().ConfigureAwait(false);

            var allEntities = await context.Set<TEntity>().ToListAsync().ConfigureAwait(false);
            context.Set<TEntity>().RemoveRange(allEntities);
            await context.SaveChangesAsync().ConfigureAwait(false);

            await context.Set<TEntity>().AddRangeAsync(entities).ConfigureAwait(false);
            await context.SaveChangesAsync().ConfigureAwait(false);
        }

        private async Task ResetUsersAsync()
        {
            var users = new List<User>
            {
                new()
                {
                    Id = 1,
                    UserName = "emresever",
                    FullName = "Emre Sever",
                    Email = "emre@mail.com"
                },
                new()
                {
                    Id = 2,
                    UserName = "astopia",
                    FullName = "astopia istanbul",
                    Email = "astopia@mail.com"
                },
                new()
                {
                    Id = 3,
                    UserName = "testuser3",
                    FullName = "test user 3",
                    Email = "test@mail.com"
                }
            };

            await ResetDatasAsync(users).ConfigureAwait(false);
        }

        private async Task ResetContentAsync()
        {
            var contents = new List<Content>
            {
                new()
                {
                    Id = 1,
                    CategoryId = 1,
                    ImageUrl = string.Empty,
                    UserId = 1,
                    Languages =
                    [
                        new()
                        {
                            Title = "Title",
                            Description = "desc",
                            LanguageId = 1,
                            VariantId = new Guid("ec5fb56a-cf46-4efe-9389-70ef12f2e0e1")
                        },
                        new()
                        {
                            Title = "Title eng",
                            Description = "desc eng",
                            LanguageId = 2,
                            VariantId = new Guid("ec5fb56a-cf46-4efe-9389-70ef12f2e0e1")
                        },
                        new()
                        {
                            Title = "Title var 2",
                            Description = "desc",
                            LanguageId = 1,
                            VariantId = new Guid("ec5fb56a-cf46-4efe-9389-70ef12f2e0e2")
                        },
                        new()
                        {
                            Title = "Title eng var 2",
                            Description = "desc eng ",
                            LanguageId = 2,
                            VariantId = new Guid("ec5fb56a-cf46-4efe-9389-70ef12f2e0e2")
                        }
                    ]
                }
            };

            await ResetDatasAsync(contents).ConfigureAwait(false);
        }

        public IServiceCollection MockServiceCollection()
        {
            IServiceCollection services = new ServiceCollection();

            var configuration = GetConfiguration();

            services.AddDbContext<CmsDbContext>(p =>
            {
                p.UseNpgsql(configuration.GetConnectionString("CmsTestDb"), p => p.MigrationsAssembly("Cms.Api"));
            });

            var redisConnection = ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis"));

            services.AddSingleton<IConnectionMultiplexer>(redisConnection);

            services.ConfigureDependencyInjections();

            return services;
        }

        private IConfiguration GetConfiguration()
        {
            var appSettings = "appsettings.json";

            if (!_isProductionTest)
                appSettings = "appsettings.Development.json";

            return new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(appSettings).Build();
        }
    }
}
