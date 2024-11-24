using Cms.Data;
using Cms.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Cms.UnitTest.Utils
{
    public static class Mock
    {
        public static async Task ResetAllDatas()
        {
            await ResetUsers().ConfigureAwait(false);
            await ResetContents().ConfigureAwait(false);
        }


        private static async Task ResetDatas<TEntity>(List<TEntity> entities) where TEntity : class
        {
            IConfiguration configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .Build();

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

        private static async Task ResetUsers()
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

            await ResetDatas(users).ConfigureAwait(false);
        }

        private static async Task ResetContents()
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

            await ResetDatas(contents).ConfigureAwait(false);
        }
    }
}
