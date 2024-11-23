using Cms.Api.Cache.Abstract;
using Cms.Api.Cache.Concrate;
using Cms.Api.Filters.Concrate;
using Cms.Api.Services.Abstract;
using Cms.Common.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        private readonly IContentService _contentService;
        private readonly ICacheService _cacheService;

        public ContentController(IContentService contentService, ICacheService cacheService)
        {
            _contentService = contentService;
            _cacheService = cacheService;
        }

        [HttpGet("{signedUserId}/{contentId}/{languageId}")]
        public async Task<IActionResult> GetContentByIdAsync(int signedUserId, int contentId, int languageId)
        {
            var users = await _cacheService.ConfigureSetGetAsync(CacheService.GetCacheKey(HttpContext, prefix: CacheService.ContentPrefix), async () =>
            {
                return await _contentService.GetContentByIdAsync(signedUserId, contentId, languageId).ConfigureAwait(false);
            });

            return users.GetApiResponse(HttpContext);
        }

        [HttpPut("contents/{signedUserId}")]
        public async Task<IActionResult> GetContentsByUserAsync(int signedUserId, ContentFilter contentFilter)
        {
            var users = await _cacheService.ConfigureSetGetAsync(CacheService.GetCacheKey(HttpContext, contentFilter, prefix: CacheService.ContentPrefix), async () =>
            {
                return await _contentService.GetContentsByUserAsync(signedUserId, contentFilter).ConfigureAwait(false);
            });

            return users.GetApiResponse(HttpContext);
        }
    }
}
