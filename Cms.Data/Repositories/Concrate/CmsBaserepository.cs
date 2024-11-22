using Cms.Data.Includable;
using Cms.Data.Repositories.Abstract;
using Cms.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cms.Data.Repositories.Concrate
{
    public class CmsBaserepository<TEntity>(CmsDbContext dbContext) : ICmsBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly CmsDbContext _dbContext = dbContext;
        protected readonly DbSet<TEntity> _dbSet = dbContext.Set<TEntity>();

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> conditionExpression = null, Expression<Func<TEntity, TEntity>> projectionExpression = null, bool tracking = false)
        {
            return await _dbSet.AsTracking(GetQueryTrackingBehavior(tracking))
                               .Where(conditionExpression ?? (((TEntity entity) => true)))
                               .Select(projectionExpression ?? (((TEntity entity) => entity)))
                               .ToListAsync()
                               .ConfigureAwait(continueOnCapturedContext: false);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Func<IIncludable<TEntity>, IIncludable> includes, Expression<Func<TEntity, bool>> conditionExpression = null, Expression<Func<TEntity, TEntity>> projectionExpression = null, bool tracking = false)
        {
            return await _dbSet.AsTracking(GetQueryTrackingBehavior(tracking))
                               .Where(conditionExpression ?? (((TEntity entity) => true)))
                               .IncludeMultiple(includes)
                               .Select(projectionExpression ?? (((TEntity entity) => entity)))
                               .ToListAsync()
                               .ConfigureAwait(continueOnCapturedContext: false);
        }

        public async Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> conditionExpression = null, Expression<Func<TEntity, TEntity>> projectionExpression = null, bool tracking = false)
        {
            return await _dbSet.AsTracking(GetQueryTrackingBehavior(tracking))
                               .Select(projectionExpression ?? ((TEntity entity) => entity))
                               .FirstOrDefaultAsync(conditionExpression ?? ((TEntity entity) => true))
                               .ConfigureAwait(continueOnCapturedContext: false);
        }

        public async Task<TEntity?> GetFirstOrDefaultAsync(Func<IIncludable<TEntity>, IIncludable> includes, Expression<Func<TEntity, bool>> conditionExpression = null, Expression<Func<TEntity, TEntity>> projectionExpression = null, bool tracking = false)
        {
            return await _dbSet.AsTracking(GetQueryTrackingBehavior(tracking))
                               .IncludeMultiple(includes)
                               .Select(projectionExpression ?? ((TEntity entity) => entity))
                               .FirstOrDefaultAsync(conditionExpression ?? ((TEntity entity) => true))
                               .ConfigureAwait(continueOnCapturedContext: false);
        }

        public async Task<(IEnumerable<TEntity> entities, int pageCount, int totalDataCount)> GetAsPaginatedAsync(int requestedPageNumber, int countOfRequestedRecordsInPage, Expression<Func<TEntity, bool>> conditionExpression = null, bool tracking = false)
        {
            throw new NotImplementedException();
        }

        public async Task<(IEnumerable<TEntity> entities, int pageCount, int totalDataCount)> GetAsPaginatedAsync(int requestedPageNumber, int countOfRequestedRecordsInPage, Func<IIncludable<TEntity>, IIncludable> includes, Expression<Func<TEntity, bool>> conditionExpression = null, bool tracking = false)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> GetByIdAsync(int id, Expression<Func<TEntity, bool>> conditionExpression = null, Expression<Func<TEntity, TEntity>> projectionExpression = null, bool tracking = false)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> GetByIdAsync(int id, Func<IIncludable<TEntity>, IIncludable> includes, Expression<Func<TEntity, bool>> conditionExpression = null, Expression<Func<TEntity, TEntity>> projectionExpression = null, bool tracking = false)
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }


        private static QueryTrackingBehavior GetQueryTrackingBehavior(bool tracking)
        {
            if (!tracking)
            {
                return QueryTrackingBehavior.NoTracking;
            }

            return QueryTrackingBehavior.TrackAll;
        }
    }
}
