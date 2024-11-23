using Cms.Api.Cache.Abstract;
using Cms.Api.Cache.Concrate;
using Cms.Api.DTO;
using Cms.Api.Filters.Concrate;
using Cms.Api.Services.Abstract;
using Cms.Common.Exceptions;
using Cms.Common.Helpers;
using Cms.Data.Includable;
using Cms.Data.Repositories.Abstract;
using Cms.Entity;
using Mapster;

namespace Cms.Api.Services.Concrate
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IContentRepository _contentRepository;
        private readonly ICmsBaseRepository<ContentLanguage> _contentLanguageRepository;
        private readonly ICacheService _cacheService;

        public UserService(IUserRepository userRepository, IContentRepository contentRepository, ICmsBaseRepository<ContentLanguage> contentLanguageRepository, ICacheService cacheService)
        {
            _userRepository = userRepository;
            _contentRepository = contentRepository;
            _contentLanguageRepository = contentLanguageRepository;
            _cacheService = cacheService;
        }

        public async Task AddUserAsync(AddUserDto userDto)
        {
            var user = userDto.Adapt<User>();

            await _userRepository.AddAsync(user).ConfigureAwait(false);

            await _cacheService.RemoveByPrefixAsync(CacheService.UserPrefix).ConfigureAwait(false);
        }

        public async Task<IEnumerable<UserDto>> ListUsersAsync(UserFilter userFilter)
        {
            var users = await _userRepository.GetAllAsync(userFilter.CreateFilterExpression()).ConfigureAwait(false);

            if (users.IsNullOrEmpty())
                throw new CmsApiException("Sistemde kullanici bulunamamistir.");

            return users.Adapt<IEnumerable<UserDto>>();
        }

        public async Task<UserDto> GetContentsByUserAsync(int userId)
        {
            var user = (await _userRepository.GetByIdAsync(userId, p => p.Include(p => p.Contens).ThenInclude(p => p.Languages)).ConfigureAwait(false))
                       ?? throw new CmsApiException("Secmis oldugunuz kullanici sisteme kayitli degildir.");

            if (user.Contens.IsNullOrEmpty())
                throw new CmsApiException("Secmis oldugunuz kullaniciya ait bir icerik bulunmamaktadir.");

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                FullName = user.FullName,
                Contens = (from p in user.Contens
                           select new ContentDto
                           {
                               Id = p.Id,
                               CategoryId = p.CategoryId,
                               ImageUrl = p.ImageUrl,
                               UpdatedAt = p.CreatedAt,
                               CreatedAt = p.CreatedAt,
                               Languages = p.Languages?.Select(l => new ContentLanguageDto
                               {
                                   Id = l.Id,
                                   Title = l.Title,
                                   Description = l.Description,
                                   LanguageId = l.LanguageId,
                                   VariantId = l.VariantId
                               }).OrderBy(p => p.VariantId).ThenBy(p => p.LanguageId).ToList() ?? []
                           })
            };
        }

        public async Task AddContentAsync(AddContentDto addContentDto)
        {
            if (addContentDto == null)
                throw new CmsApiException("Gecersiz data.");

            if (addContentDto?.UserId <= 0)
                throw new CmsApiException("Lutfen gecerli bir kullanici seciniz.");

            if ((addContentDto?.ContentVariants.Count() ?? 0) < 2)
                throw new CmsApiException("Lutfen en az 2 varyant giriniz.");

            if (!await _userRepository.ExistsAsync(addContentDto.UserId))
                throw new CmsApiException("Lutfen gecerli bir kullanici seciniz.");

            var content = new Content()
            {
                CategoryId = addContentDto.CategoryId,
                ImageUrl = addContentDto.ImageUrl,
                UserId = addContentDto.UserId
            };

            List<ContentLanguage> languages = new();

            Guid variantId = Guid.Empty;

            foreach (var variants in addContentDto.ContentVariants)
            {
                variantId = Guid.NewGuid();

                languages.AddRange(variants.Select(p => new ContentLanguage()
                {
                    Title = p.Title,
                    Description = p.Description,
                    LanguageId = p.LanguageId,
                    VariantId = variantId
                }));
            }

            content.Languages = languages;

            await _contentRepository.AddAsync(content).ConfigureAwait(false);

            await _cacheService.RemoveByPrefixAsync($"{CacheService.ContentPrefix}-{addContentDto.UserId}").ConfigureAwait(false);
        }

        public async Task AddVariantToContentAsync(AddContentVariantDto addContentVariantDto)
        {
            addContentVariantDto ??= new();

            if (!await _contentRepository.ExistsAsync(addContentVariantDto.ContentId))
                throw new CmsApiException("Lutfen gecerli bir icerik seciniz.");

            if (addContentVariantDto.ContentVariants.IsNullOrEmpty())
                throw new CmsApiException("Lutfen en az 2 varyant giriniz.");

            Guid variantId = Guid.Empty;

            List<ContentLanguage> languages = [];

            foreach (var variants in addContentVariantDto.ContentVariants)
            {
                variantId = Guid.NewGuid();

                languages.AddRange(variants.Select(p => new ContentLanguage()
                {
                    Title = p.Title,
                    Description = p.Description,
                    LanguageId = p.LanguageId,
                    ContentId = addContentVariantDto.ContentId,
                    VariantId = variantId
                }));
            }

            await _contentLanguageRepository.AddRangeAsync(languages).ConfigureAwait(false);

            await _cacheService.RemoveByPrefixAsync($"{CacheService.ContentPrefix}").ConfigureAwait(false);
        }

        public async Task DeleteContentAsync(int contentId)
        {
            var content = await _contentRepository.GetByIdAsync(contentId).ConfigureAwait(false) ?? throw new CmsApiException("Lutfen gecerli bir icerik seciniz.");

            await _contentRepository.DeleteAsync(content).ConfigureAwait(false);
        }
    }
}
