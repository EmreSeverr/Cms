using System.Linq.Expressions;

namespace Cms.Api.Filters.Abstract
{
    public interface IBaseFilter<TEntity> where TEntity : class
    {
        Expression<Func<TEntity, bool>> CreateFilterExpression();
    }
}
