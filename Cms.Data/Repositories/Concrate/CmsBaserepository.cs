using Cms.Common.Exceptions;
using Cms.Common.Helpers;
using Cms.Data.Includable;
using Cms.Data.Repositories.Abstract;
using Cms.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace Cms.Data.Repositories.Concrate
{
    public class CmsBaseRepository<TEntity>(CmsDbContext dbContext) : ICmsBaseRepository<TEntity> where TEntity : BaseEntity
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

        public async Task<TEntity?> GetFirstOrDefaultOrderedAsync(string orderByPropertyName, bool orderByAscending, Expression<Func<TEntity, bool>> conditionExpression = null, Expression<Func<TEntity, TEntity>> projectionExpression = null, bool tracking = false)
        {
            CheckProperty(orderByPropertyName);

            Expression<Func<TEntity, object>> predicate = CreateObjectPredicate(typeof(TEntity), orderByPropertyName);

            return !orderByAscending ? await _dbSet.AsTracking(GetQueryTrackingBehavior(tracking))
                                                   .Select(projectionExpression ?? ((TEntity entity) => entity))
                                                   .OrderByDescending(predicate)
                                                   .FirstOrDefaultAsync(conditionExpression ?? ((TEntity entity) => true))
                                                   .ConfigureAwait(continueOnCapturedContext: false)
                                     : await _dbSet.AsTracking(GetQueryTrackingBehavior(tracking))
                                                   .Select(projectionExpression ?? ((TEntity entity) => entity))
                                                   .OrderBy(predicate)
                                                   .FirstOrDefaultAsync(conditionExpression ?? ((TEntity entity) => true))
                                                   .ConfigureAwait(continueOnCapturedContext: false);
        }

        public async Task<TEntity?> GetFirstOrDefaultOrderedAsync(string orderByPropertyName, bool orderByAscending, Func<IIncludable<TEntity>, IIncludable> includes, Expression<Func<TEntity, bool>> conditionExpression = null, Expression<Func<TEntity, TEntity>> projectionExpression = null, bool tracking = false)
        {
            CheckProperty(orderByPropertyName);

            Expression<Func<TEntity, object>> predicate = CreateObjectPredicate(typeof(TEntity), orderByPropertyName);

            return !orderByAscending ? await _dbSet.AsTracking(GetQueryTrackingBehavior(tracking))
                                                   .IncludeMultiple(includes)
                                                   .Select(projectionExpression ?? ((TEntity entity) => entity))
                                                   .OrderByDescending(predicate)
                                                   .FirstOrDefaultAsync(conditionExpression ?? ((TEntity entity) => true))
                                                   .ConfigureAwait(continueOnCapturedContext: false)
                                     : await _dbSet.AsTracking(GetQueryTrackingBehavior(tracking))
                                                   .IncludeMultiple(includes)
                                                   .Select(projectionExpression ?? ((TEntity entity) => entity))
                                                   .OrderBy(predicate)
                                                   .FirstOrDefaultAsync(conditionExpression ?? ((TEntity entity) => true))
                                                   .ConfigureAwait(continueOnCapturedContext: false);
        }

        public async Task<(IEnumerable<TEntity> entities, int pageCount, int totalDataCount)> GetAsPaginatedAsync(int requestedPageNumber, int countOfRequestedRecordsInPage, Expression<Func<TEntity, bool>> conditionExpression = null, bool tracking = false)
        {
            ValidatePaginationParameters(requestedPageNumber, countOfRequestedRecordsInPage);

            int totalDataCount = await GetCountAsync(conditionExpression).ConfigureAwait(continueOnCapturedContext: false);

            List<TEntity> item = await _dbSet.AsTracking(GetQueryTrackingBehavior(tracking))
                                             .Where(conditionExpression ?? ((TEntity entity) => true)).Skip((requestedPageNumber - 1) * countOfRequestedRecordsInPage)
                                             .Take(countOfRequestedRecordsInPage)
                                             .ToListAsync()
                                             .ConfigureAwait(continueOnCapturedContext: false);

            int item2 = CalculatePageCountAndCompareWithRequested(totalDataCount, countOfRequestedRecordsInPage, requestedPageNumber);

            return (item, item2, totalDataCount);
        }

        public async Task<(IEnumerable<TEntity> entities, int pageCount, int totalDataCount)> GetAsPaginatedAsync(int requestedPageNumber, int countOfRequestedRecordsInPage, Func<IIncludable<TEntity>, IIncludable> includes, Expression<Func<TEntity, bool>> conditionExpression = null, bool tracking = false)
        {
            ValidatePaginationParameters(requestedPageNumber, countOfRequestedRecordsInPage);

            int totalDataCount = await GetCountAsync(conditionExpression).ConfigureAwait(continueOnCapturedContext: false);

            List<TEntity> item = await _dbSet.AsTracking(GetQueryTrackingBehavior(tracking))
                                             .Where(conditionExpression ?? ((TEntity entity) => true)).Skip((requestedPageNumber - 1) * countOfRequestedRecordsInPage)
                                             .IncludeMultiple(includes)
                                             .Take(countOfRequestedRecordsInPage)
                                             .ToListAsync()
                                             .ConfigureAwait(continueOnCapturedContext: false);

            int item2 = CalculatePageCountAndCompareWithRequested(totalDataCount, countOfRequestedRecordsInPage, requestedPageNumber);

            return (item, item2, totalDataCount);
        }

        public async Task<TEntity> GetByIdAsync(int id, Expression<Func<TEntity, bool>> conditionExpression = null, Expression<Func<TEntity, TEntity>> projectionExpression = null, bool tracking = false)
        {
            return await _dbSet.AsTracking(GetQueryTrackingBehavior(tracking))
                               .Select(projectionExpression ?? (((TEntity entity) => entity)))
                               .SingleOrDefaultAsync(p => p.Id == id)
                               .ConfigureAwait(continueOnCapturedContext: false);
        }

        public async Task<TEntity> GetByIdAsync(int id, Func<IIncludable<TEntity>, IIncludable> includes, Expression<Func<TEntity, bool>> conditionExpression = null, Expression<Func<TEntity, TEntity>> projectionExpression = null, bool tracking = false)
        {
            return await _dbSet.AsTracking(GetQueryTrackingBehavior(tracking))
                               .IncludeMultiple(includes)
                               .Select(projectionExpression ?? (((TEntity entity) => entity)))
                               .SingleOrDefaultAsync(p => p.Id == id)
                               .ConfigureAwait(continueOnCapturedContext: false);
        }

        public virtual async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> conditionExpression = null, bool tracking = false) =>
            await _dbSet.AsTracking(GetQueryTrackingBehavior(tracking)).Where(conditionExpression ?? ((TEntity entity) => true)).CountAsync().ConfigureAwait(continueOnCapturedContext: false);

        public virtual async Task<bool> ExistsAsync(int id)
        {
            return await _dbSet.AsNoTracking().SingleOrDefaultAsync(p => p.Id == id).ConfigureAwait(continueOnCapturedContext: false) != null;
        }
        public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> conditionExpression)
        {
            return await _dbSet.AsNoTracking().SingleOrDefaultAsync(conditionExpression).ConfigureAwait(continueOnCapturedContext: false) != null;
        }


        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity).ConfigureAwait(continueOnCapturedContext: false);

            await _dbContext.SaveChangesAsync().ConfigureAwait(continueOnCapturedContext: false);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities).ConfigureAwait(continueOnCapturedContext: false);

            await _dbContext.SaveChangesAsync().ConfigureAwait(continueOnCapturedContext: false);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);

            await _dbContext.SaveChangesAsync().ConfigureAwait(continueOnCapturedContext: false);
        }

        public async Task UpdateAsync(IEnumerable<TEntity> entities)
        {
            if (!entities.IsNullOrEmpty())
            {
                _dbSet.UpdateRange(entities);

                await _dbContext.SaveChangesAsync().ConfigureAwait(continueOnCapturedContext: false);
            }
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);

            await _dbContext.SaveChangesAsync().ConfigureAwait(continueOnCapturedContext: false);
        }

        public async Task DeleteAsync(IEnumerable<TEntity> entities)
        {
            if (!entities.IsNullOrEmpty())
            {
                _dbSet.RemoveRange(entities);

                await _dbContext.SaveChangesAsync().ConfigureAwait(continueOnCapturedContext: false);
            }
        }


        private static QueryTrackingBehavior GetQueryTrackingBehavior(bool tracking)
        {
            if (!tracking)
            {
                return QueryTrackingBehavior.NoTracking;
            }

            return QueryTrackingBehavior.TrackAll;
        }

        private static int CalculatePageCountAndCompareWithRequested(int totalDataCount, int countOfRequestedRecordsInPage, int requestedPageNumber)
        {
            int num = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(totalDataCount) / Convert.ToDouble(countOfRequestedRecordsInPage)));

            if (num != 0 && requestedPageNumber > num)
            {
                throw new Exception();
            }

            return num;
        }

        protected static void ValidatePaginationParameters(int requestedPageNumber, int countOfRequestedRecordsInPage)
        {
            if (requestedPageNumber <= 0)
            {
                throw new Exception();
            }

            if (countOfRequestedRecordsInPage <= 0)
            {
                throw new Exception();
            }
        }

        protected Expression<Func<TEntity, object>> CreateObjectPredicate(Type entityType, string propertyName)
        {
            ParameterExpression parameterExpression = Expression.Parameter(entityType, "i");
            return Expression.Lambda<Func<TEntity, object>>(Expression.Convert(Expression.Property(parameterExpression, propertyName), typeof(object)), [parameterExpression]);
        }

        protected static void CheckProperty(string propertyName)
        {
            if (!(typeof(TEntity).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public) != null))
            {
                throw new CmsApiException();
            }
        }
    }
}
