namespace Cms.Api.Transaction.Abstract
{
    public interface ICmsTransaction
    {
        Task LaunchTransactionAsync(Func<Task> func);

        Task LaunchTransactionAsync(Func<Task> func, Func<Task> rollbackFunction);

        Task LaunchTransactionAsync(Func<Task> func, Action rollbackFunction);

        Task<TResult> LaunchTransactionAsync<TResult>(Func<Task<TResult>> func);

        Task<TResult> LaunchTransactionAsync<TResult>(Func<Task<TResult>> func, Func<Task> rollbackFunction);

        Task<TResult> LaunchTransactionAsync<TResult>(Func<Task<TResult>> func, Action rollbackFunction);
    }
}
