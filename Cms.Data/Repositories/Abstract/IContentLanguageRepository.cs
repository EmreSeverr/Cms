using Cms.Entity;
using System.Linq.Expressions;

namespace Cms.Data.Repositories.Abstract
{
    public interface IContentLanguageRepository : ICmsBaseRepository<ContentLanguage>
    {
        Task<IEnumerable<ContentLanguage>> GetNewContentsForUser(IEnumerable<int> contentHistoryIds, Expression<Func<ContentLanguage, bool>> conditionExpression = null);
    }
}
