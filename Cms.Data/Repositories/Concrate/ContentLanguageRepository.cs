using Cms.Common.Helpers;
using Cms.Data.Repositories.Abstract;
using Cms.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cms.Data.Repositories.Concrate
{
    public class ContentLanguageRepository : CmsBaseRepository<ContentLanguage>, IContentLanguageRepository
    {
        private readonly DbSet<ContentLanguage> _dbSet;

        public ContentLanguageRepository(CmsDbContext cmsDbContext) : base(cmsDbContext)
        {
            _dbSet = cmsDbContext.Set<ContentLanguage>();
        }


        public async Task<IEnumerable<ContentLanguage>> GetNewContentsForUser(IEnumerable<int> contentHistoryIds, Expression<Func<ContentLanguage, bool>> conditionExpression)
        {
            var newContents = await _dbSet.Include(p => p.Content).ThenInclude(p => p.VariantHistories)
                                          .Where(conditionExpression)
                                          .Where(p => !contentHistoryIds.Contains(p.ContentId))
                                          .GroupBy(p => p.ContentId).ToListAsync().ConfigureAwait(false);

            if (newContents.IsNullOrEmpty())
                return [];


            return from p in newContents
                   let newContent = p.OrderBy(p => p.CreatedAt).FirstOrDefault()
                   select new ContentLanguage
                   {
                       Id = newContent.Id,
                       ContentId = newContent.ContentId,
                       CreatedAt = newContent.CreatedAt,
                       Description = newContent.Description,
                       Title = newContent.Title,
                       LanguageId = newContent.LanguageId,
                       VariantId = newContent.VariantId,
                       Content = newContent.Content
                   };
        }
    }
}
