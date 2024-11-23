namespace Cms.Api.Transaction.Abstract
{
    public interface ICmsTransaction
    {
        /// <summary>
        /// It runs transaction.
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        Task LaunchTransactionAsync(Func<Task> func);

        /// <summary>
        /// It runs transaction.
        /// </summary>
        /// <param name="func"></param>
        /// <param name="rollbackFunction"></param>
        /// <returns></returns>
        Task LaunchTransactionAsync(Func<Task> func, Func<Task> rollbackFunction);

        /// <summary>
        /// It runs transaction.
        /// </summary>
        /// <param name="func"></param>
        /// <param name="rollbackFunction"></param>
        /// <returns></returns>
        Task LaunchTransactionAsync(Func<Task> func, Action rollbackFunction);

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
        Task<TResult> LaunchTransactionAsync<TResult>(Func<Task<TResult>> func);

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
        Task<TResult> LaunchTransactionAsync<TResult>(Func<Task<TResult>> func, Func<Task> rollbackFunction);

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
        Task<TResult> LaunchTransactionAsync<TResult>(Func<Task<TResult>> func, Action rollbackFunction);
    }
}
