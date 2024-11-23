using Cms.Api.Transaction.Abstract;
using Cms.Data;

namespace Cms.Api.Transaction.Concrate
{
    public class CmsTransaction : ICmsTransaction
    {
        private readonly CmsDbContext _cmsDbContext;

        /// <summary>
        /// Constructor of <see cref="CmsTransaction"/>.
        /// </summary>
        /// <param name="marketDbContext"></param>
        public CmsTransaction(CmsDbContext cmsDbContext)
        {
            _cmsDbContext = cmsDbContext;
        }

        /// <summary>
        /// It runs transaction.
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
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

        /// <summary>
        /// It runs transaction.
        /// </summary>
        /// <param name="func"></param>
        /// <param name="rollbackFunction"></param>
        /// <returns></returns>
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

        /// <summary>
        /// It runs transaction.
        /// </summary>
        /// <param name="func"></param>
        /// <param name="rollbackFunction"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// It runs transaction.
        /// 
        /// <para> If the method you are using has a return value, you can use this method. </para>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// It runs transaction.
        /// 
        /// <para> If the method you are using has a return value, you can use this method. </para>
        /// 
        /// </summary>
        /// <param name="func"></param>
        /// <param name="rollbackFunction"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// It runs transaction.
        /// 
        /// <para> If the method you are using has a return value, you can use this method. </para>
        /// 
        /// </summary>
        /// <param name="func"></param>
        /// <param name="rollbackFunction"></param>
        /// <returns></returns>
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
