using Cms.Api.DTO;
using Cms.Api.Filters.Concrate;
using Cms.Api.Services.Abstract;
using Cms.Common.Exceptions;
using Cms.Data.Includable;
using Cms.Data.Repositories.Abstract;
using Cms.UnitTest.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace Cms.UnitTest.Tests
{
    public class UserTests
    {
        private readonly Mock _mock;
        private readonly IUserService _userService;
        private readonly IContentRepository _contentRepository;

        public UserTests()
        {
            _mock = new Mock();

            var serviceCollection = _mock.MockServiceCollection();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            _userService = serviceProvider.GetRequiredService<IUserService>();
            _contentRepository = serviceProvider.GetRequiredService<IContentRepository>();

            _mock.ResetAllDatasAsync().Wait();
        }

        [Fact]
        public async Task AddUserTestAsync()
        {
            var user = new AddUserDto
            {
                UserName = "serkan",
                FullName = "Test Service",
                Email = "testservice@mail.com"
            };

            await _userService.AddUserAsync(user);

            var userCount = await _userService.ListUsersAsync(new UserFilter());

            var expectedUserCount = 4;

            Assert.Equal(expectedUserCount, userCount.Count());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetContentsByUserTestAsync(int userId)
        {
            var contents = await _userService.GetContentsByUserAsync(userId);

            if (userId == 1)
                Assert.Equal(1, contents.Contens?.Count());
            else
                Assert.Equal(0, contents.Contens?.Count());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task AddContentTestAsync(int userId)
        {
            var contentDto = new AddContentDto
            {
                CategoryId = 1,
                UserId = userId,
                ContentVariants =
                [
                    [//Variant-1
                        new()
                        {
                            Title = "Test",
                            Description = "Test",
                            LanguageId = 1
                        },
                        new()
                        {
                            Title = "Test",
                            Description = "Test",
                            LanguageId = 2
                        }
                    ],
                    [//Variant-2
                        new()
                        {
                            Title = "Test",
                            Description = "Test",
                            LanguageId = 1
                        },
                        new()
                        {
                            Title = "Test",
                            Description = "Test",
                            LanguageId = 2
                        }
                    ],

                    [//Variant-3
                        new()
                        {
                            Title = "Test",
                            Description = "Test",
                            LanguageId = 1
                        },
                        new()
                        {
                            Title = "Test",
                            Description = "Test",
                            LanguageId = 2
                        }
                    ]
                ]
            };

            await _userService.AddContentAsync(contentDto);

            var contents = await _userService.GetContentsByUserAsync(userId);

            if (userId == 1)
                Assert.Equal(2, contents.Contens?.Count());
            else
                Assert.Equal(1, contents.Contens?.Count());
        }

        [Fact]
        public async Task AddContentUnlessVariantsTestAsync()
        {
            var contentDto = new AddContentDto
            {
                CategoryId = 1,
                UserId = 1,
                ContentVariants =
                [
                    [//Variant-1
                        new()
                        {
                            Title = "Test",
                            Description = "Test",
                            LanguageId = 1
                        },
                        new()
                        {
                            Title = "Test",
                            Description = "Test",
                            LanguageId = 2
                        }
                    ]
                ]
            };

            await Assert.ThrowsAsync<CmsApiException>(async () => await _userService.AddContentAsync(contentDto));
        }

        [Fact]
        public async Task AddVariantToContentTestAsync()
        {
            var testContentId = 1;

            var variants = new AddContentVariantDto
            {
                ContentId = testContentId,
                ContentVariants =
                [
                    [
                        new()
                        {
                            Title = "Test",
                            Description = "Test",
                            LanguageId = 1
                        },
                        new()
                        {
                            Title = "Test",
                            Description= "Test",
                            LanguageId = 2
                        }
                    ],
                    [
                        new()
                        {
                            Title = "Test",
                            Description = "Test",
                            LanguageId = 1
                        },
                        new()
                        {
                            Title = "Test",
                            Description= "Test",
                            LanguageId = 2
                        }
                    ]
                ]
            };

            await _userService.AddVariantToContentAsync(variants);

            var content = await _contentRepository.GetByIdAsync(testContentId, p => p.Include(p => p.Languages));

            Assert.NotNull(content);
            Assert.Equal(4, content.Languages.GroupBy(p => p.VariantId).Count());
        }

        [Fact]
        public async Task AddVariantToContentNotExistContentTestAsync()
        {
            var testContentId = 100;

            var variants = new AddContentVariantDto
            {
                ContentId = testContentId,
                ContentVariants =
                [
                    [
                        new()
                        {
                            Title = "Test",
                            Description = "Test",
                            LanguageId = 1
                        },
                        new()
                        {
                            Title = "Test",
                            Description= "Test",
                            LanguageId = 2
                        }
                    ],
                    [
                        new()
                        {
                            Title = "Test",
                            Description = "Test",
                            LanguageId = 1
                        },
                        new()
                        {
                            Title = "Test",
                            Description= "Test",
                            LanguageId = 2
                        }
                    ]
                ]
            };

            await Assert.ThrowsAsync<CmsApiException>(async () => await _userService.AddVariantToContentAsync(variants));
        }
    }
}
