using Cms.Api.Filters.Abstract;
using Cms.Common.Helpers;
using Cms.Entity;
using System.Linq.Expressions;

namespace Cms.Api.Filters.Concrate
{
    public class UserFilter : IBaseFilter<User>
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }

        public Expression<Func<User, bool>> CreateFilterExpression()
        {
            Expression<Func<User, bool>> mainExpression = null;
            List<Expression<Func<User, bool>>> predicates = new();

            if (!string.IsNullOrEmpty(UserName)) predicates.Add(p => p.UserName == UserName);
            if (!string.IsNullOrEmpty(Email)) predicates.Add(p => p.Email == Email);

            predicates?.ForEach(predicate => mainExpression = mainExpression.Append(predicate, ExpressionType.AndAlso));

            return mainExpression;
        }
    }
}
