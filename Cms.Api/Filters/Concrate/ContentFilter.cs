using Cms.Api.Filters.Abstract;
using Cms.Common.Helpers;
using Cms.Entity;
using System.Linq.Expressions;

namespace Cms.Api.Filters.Concrate
{
    public class ContentFilter : IBaseFilter<Content>
    {
        public int? CategoryId { get; set; }
        public int? UserId { get; set; }
        public int LanguageId { get; set; } = 1;

        public Expression<Func<Content, bool>> CreateFilterExpression()
        {
            Expression<Func<Content, bool>> mainExpression = null;
            List<Expression<Func<Content, bool>>> predicates = new();

            predicates.Add(p => p.Languages.Any(p => p.LanguageId == LanguageId));

            if (CategoryId.HasValue) predicates.Add(p => p.CategoryId == CategoryId);
            if (UserId.HasValue) predicates.Add(p => p.UserId == UserId);

            predicates?.ForEach(predicate => mainExpression = mainExpression.Append(predicate, ExpressionType.AndAlso));

            return mainExpression;
        }
    }
}
