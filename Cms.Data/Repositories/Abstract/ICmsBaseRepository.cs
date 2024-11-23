using Cms.Data.Includable;
using Cms.Entity;
using System.Linq.Expressions;

namespace Cms.Data.Repositories.Abstract
{
    public interface ICmsBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> conditionExpression = null, Expression<Func<TEntity, TEntity>> projectionExpression = null, bool tracking = false);
        Task<IEnumerable<TEntity>> GetAllAsync(Func<IIncludable<TEntity>, IIncludable> includes, Expression<Func<TEntity, bool>> conditionExpression = null, Expression<Func<TEntity, TEntity>> projectionExpression = null, bool tracking = false);

        Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> conditionExpression = null, Expression<Func<TEntity, TEntity>> projectionExpression = null, bool tracking = false);
        Task<TEntity?> GetFirstOrDefaultAsync(Func<IIncludable<TEntity>, IIncludable> includes, Expression<Func<TEntity, bool>> conditionExpression = null, Expression<Func<TEntity, TEntity>> projectionExpression = null, bool tracking = false);

        Task<TEntity?> GetFirstOrDefaultOrderedAsync(string orderByPropertyName, bool orderByAscending, Expression<Func<TEntity, bool>> conditionExpression = null, Expression<Func<TEntity, TEntity>> projectionExpression = null, bool tracking = false);
        Task<TEntity?> GetFirstOrDefaultOrderedAsync(string orderByPropertyName, bool orderByAscending, Func<IIncludable<TEntity>, IIncludable> includes, Expression<Func<TEntity, bool>> conditionExpression = null, Expression<Func<TEntity, TEntity>> projectionExpression = null, bool tracking = false);

        Task<(IEnumerable<TEntity> entities, int pageCount, int totalDataCount)> GetAsPaginatedAsync(int requestedPageNumber, int countOfRequestedRecordsInPage, Expression<Func<TEntity, bool>> conditionExpression = null, bool tracking = false);
        Task<(IEnumerable<TEntity> entities, int pageCount, int totalDataCount)> GetAsPaginatedAsync(int requestedPageNumber, int countOfRequestedRecordsInPage, Func<IIncludable<TEntity>, IIncludable> includes, Expression<Func<TEntity, bool>> conditionExpression = null, bool tracking = false);

        Task<TEntity?> GetByIdAsync(int id, Expression<Func<TEntity, bool>> conditionExpression = null, Expression<Func<TEntity, TEntity>> projectionExpression = null, bool tracking = false);
        Task<TEntity?> GetByIdAsync(int id, Func<IIncludable<TEntity>, IIncludable> includes, Expression<Func<TEntity, bool>> conditionExpression = null, Expression<Func<TEntity, TEntity>> projectionExpression = null, bool tracking = false);

        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> conditionExpression);

        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> conditionExpression = null, bool tracking = false);

        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);

        Task UpdateAsync(TEntity entity);
        Task UpdateAsync(IEnumerable<TEntity> entities);

        Task DeleteAsync(TEntity entity);
        Task DeleteAsync(IEnumerable<TEntity> entities);
    }
}
