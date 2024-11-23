namespace Cms.Api.Cache.Abstract
{
    public interface ICacheService
    {
        Task<TEntity> ConfigureSetGetAsync<TEntity>(string key, Func<Task<TEntity>> func, TimeSpan? offset = null) where TEntity : class;
        Task<IEnumerable<TEntity>> ConfigureSetGetAsync<TEntity>(string key, Func<Task<IEnumerable<TEntity>>> func, TimeSpan? offset = null) where TEntity : class;
        Task Clear(string key);
        Task ClearAll();
    }
}
