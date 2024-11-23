using Cms.Api.Transaction.Abstract;
using Cms.Data;

namespace Cms.Api.Transaction.Concrate
{
    public class CmsTransaction : ICmsTransaction
    {
        private readonly CmsDbContext _cmsDbContext;

        public CmsTransaction(CmsDbContext cmsDbContext)
        {
            _cmsDbContext = cmsDbContext;
        }

        public async Task LaunchTransactionAsync(Func<Task> func)
        {
            using var transaction = await _cmsDbContext.Database.BeginTransactionAsync().ConfigureAwait(false);

            try
            {
                await func().ConfigureAwait(false);
                await transaction.CommitAsync().ConfigureAwait(false);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync().ConfigureAwait(false);
                throw;
            }
        }

        public async Task LaunchTransactionAsync(Func<Task> func, Func<Task> rollbackFunction)
        {
            using var transaction = await _cmsDbContext.Database.BeginTransactionAsync().ConfigureAwait(false);

            try
            {
                await func().ConfigureAwait(false);
                await transaction.CommitAsync().ConfigureAwait(false);
            }
            catch (Exception)
            {
                await rollbackFunction().ConfigureAwait(false);
                await transaction.RollbackAsync().ConfigureAwait(false);
                throw;
            }
        }

        public async Task LaunchTransactionAsync(Func<Task> func, Action rollbackFunction)
        {
            using var transaction = await _cmsDbContext.Database.BeginTransactionAsync().ConfigureAwait(false);

            try
            {
                await func().ConfigureAwait(false);
                await transaction.CommitAsync().ConfigureAwait(false);
            }
            catch (Exception)
            {
                rollbackFunction();
                await transaction.RollbackAsync().ConfigureAwait(false);
                throw;
            }
        }

        public async Task<TResult> LaunchTransactionAsync<TResult>(Func<Task<TResult>> func)
        {
            using var transaction = await _cmsDbContext.Database.BeginTransactionAsync().ConfigureAwait(false);

            try
            {
                var result = await func().ConfigureAwait(false);
                await transaction.CommitAsync().ConfigureAwait(false);

                return result;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync().ConfigureAwait(false);

                throw;
            }
        }

        public async Task<TResult> LaunchTransactionAsync<TResult>(Func<Task<TResult>> func, Func<Task> rollbackFunction)
        {
            using var transaction = await _cmsDbContext.Database.BeginTransactionAsync().ConfigureAwait(false);

            try
            {
                var result = await func().ConfigureAwait(false);
                await transaction.CommitAsync().ConfigureAwait(false);

                return result;
            }
            catch (Exception)
            {
                await rollbackFunction().ConfigureAwait(false);
                await transaction.RollbackAsync().ConfigureAwait(false);
                throw;
            }
        }

        public async Task<TResult> LaunchTransactionAsync<TResult>(Func<Task<TResult>> func, Action rollbackFunction)
        {
            using var transaction = await _cmsDbContext.Database.BeginTransactionAsync().ConfigureAwait(false);

            try
            {
                var result = await func().ConfigureAwait(false);
                await transaction.CommitAsync().ConfigureAwait(false);

                return result;
            }
            catch (Exception)
            {
                rollbackFunction();
                await transaction.RollbackAsync().ConfigureAwait(false);
                throw;
            }
        }
    }
}
