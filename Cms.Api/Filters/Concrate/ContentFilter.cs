using Cms.Api.Filters.Abstract;
using Cms.Common.Helpers;
using Cms.Entity;
using System.Linq.Expressions;

namespace Cms.Api.Filters.Concrate
{
    public class ContentFilter : IBaseFilter<ContentLanguage>
    {
        public int? CategoryId { get; set; }
        public int? UserId { get; set; }
        public int LanguageId { get; set; } = 1;

        public Expression<Func<ContentLanguage, bool>> CreateFilterExpression()
        {
            Expression<Func<ContentLanguage, bool>> mainExpression = null;
            List<Expression<Func<ContentLanguage, bool>>> predicates = new();

            predicates.Add(p => p.LanguageId == LanguageId);

            if (CategoryId.HasValue) predicates.Add(p => p.Content.CategoryId == CategoryId);
            if (UserId.HasValue) predicates.Add(p => p.Content.UserId == UserId);

            predicates?.ForEach(predicate => mainExpression = mainExpression.Append(predicate, ExpressionType.AndAlso));

            return mainExpression;
        }
    }
}
