using Cms.Api.DTO;
using Cms.Api.Filters.Concrate;
using Cms.Api.Services.Abstract;
using Cms.Api.Transaction.Abstract;
using Cms.Common.Exceptions;
using Cms.Common.Helpers;
using Cms.Data.Includable;
using Cms.Data.Repositories.Abstract;
using Cms.Entity;

namespace Cms.Api.Services.Concrate
{
    public class ContentService : IContentService
    {
        private readonly IContentRepository _contentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IVariantHistoryRepository _variantHistoryRepository;
        private readonly IContentLanguageRepository _contentLanguageRepository;
        private readonly ICmsTransaction _cmsTransaction;

        public ContentService(IContentRepository contentRepository,
                              IUserRepository userRepository,
                              IVariantHistoryRepository variantHistoryRepository,
                              IContentLanguageRepository contentLanguageRepository,
                              ICmsTransaction cmsTransaction)
        {
            _userRepository = userRepository;
            _contentRepository = contentRepository;
            _variantHistoryRepository = variantHistoryRepository;
            _contentLanguageRepository = contentLanguageRepository;
            _cmsTransaction = cmsTransaction;
        }

        public async Task<ContentListDto> GetContentByIdAsync(int signedUserId, int contentId, int languageId)
        {
            var variantHistory = await _variantHistoryRepository.GetFirstOrDefaultAsync(p => p.Include(p => p.Content).ThenInclude(p => p.Languages), p => p.UserId == signedUserId && p.ContentId == contentId);

            if (variantHistory != null)
            {
                var contentLanguage = variantHistory.Content?.Languages.FirstOrDefault(p => p.LanguageId == languageId && p.VariantId == variantHistory.VariantId) ?? throw new CmsApiException("Goruntulenmek istenen icerik bulunamamistir.");

                return new()
                {
                    Id = variantHistory.Content.Id,
                    UserId = variantHistory.Content.UserId,
                    CategoryId = variantHistory.Content.CategoryId,
                    CreatedAt = variantHistory.Content.CreatedAt,
                    Description = contentLanguage.Description,
                    ImageUrl = variantHistory.Content.ImageUrl,
                    Title = contentLanguage.Title
                };
            }
            else
            {
                return await _cmsTransaction.LaunchTransactionAsync(async () =>
                {
                    var content = await _contentLanguageRepository.GetFirstOrDefaultOrderedAsync("CreatedAt", false, p => p.Include(p => p.Content), p => p.ContentId == contentId && p.LanguageId == languageId).ConfigureAwait(false)
                                                                ?? throw new CmsApiException("Goruntulenmek istenen icerik bulunamamistir.");

                    var variantHistory = new UserContentVariantHistory
                    {
                        ContentId = contentId,
                        UserId = signedUserId,
                        VariantId = content.VariantId,
                    };

                    await _variantHistoryRepository.AddAsync(variantHistory).ConfigureAwait(false);

                    return new ContentListDto
                    {
                        Id = contentId,
                        CategoryId = content.Content.CategoryId,
                        Description = content.Description,
                        ImageUrl = content.Content.ImageUrl,
                        Title = content.Title,
                        UserId = content.Content.UserId,
                        CreatedAt = content.CreatedAt
                    };
                }).ConfigureAwait(false);
            }
        }

        public async Task<IEnumerable<ContentListDto>> GetContentsByUserAsync(int signedUserId, ContentFilter contentFilter)
        {
            var variantHistoryFiler = new VariantHistoryFilter()
            {
                CategoryId = contentFilter.CategoryId,
                LanguageId = contentFilter.LanguageId,
                SignedUserId = signedUserId,
            };

            return await _cmsTransaction.LaunchTransactionAsync(async () =>
            {
                var variantHistories = (await _variantHistoryRepository.GetAllAsync(p => p.Include(p => p.Content).ThenInclude(p => p.Languages), variantHistoryFiler.CreateFilterExpression())) ?? [];

                variantHistories = variantHistories.Where(p => p.Content?.Languages?.Count() > 0);

                var existingVariantIds = variantHistories?.Select(p => p.ContentId) ?? [];

                var newContents = (await _contentLanguageRepository.GetNewContentsForUser(existingVariantIds, contentFilter.CreateFilterExpression()).ConfigureAwait(false)) ?? [];

                //Yeni historyler db ye yaziliyor.
                if (!newContents.IsNullOrEmpty())
                {
                    var newVariantHistories = newContents.Select(p => new UserContentVariantHistory
                    {
                        ContentId = p.ContentId,
                        UserId = signedUserId,
                        VariantId = p.VariantId,
                        CreatedAt = DateTime.UtcNow
                    });

                    await _variantHistoryRepository.AddRangeAsync(newVariantHistories).ConfigureAwait(false);
                }

                return newContents.Select(p => new ContentListDto
                {
                    CategoryId = p.Content.CategoryId,
                    CreatedAt = p.CreatedAt,
                    Description = p.Description,
                    Id = p.ContentId,
                    ImageUrl = p.Content.ImageUrl,
                    Title = p.Title,
                    UserId = p.Content.UserId
                }).Concat(variantHistories.Select(p => new ContentListDto
                {
                    Id = p.ContentId,
                    UserId = p.Content.UserId,
                    ImageUrl = p.Content.ImageUrl,
                    Title = p.Content.Languages.First().Title,
                    Description = p.Content.Languages.First().Description,
                    CategoryId = p.Content.CategoryId,
                    CreatedAt = p.Content.CreatedAt,
                }));
            }).ConfigureAwait(false);
        }
    }
}
