using Cms.Api.Filters.Abstract;
using Cms.Common.Helpers;
using Cms.Entity;
using System.Linq.Expressions;

namespace Cms.Api.Filters.Concrate
{
    public class VariantHistoryFilter : IBaseFilter<UserContentVariantHistory>
    {
        public int? CategoryId { get; set; }
        public int LanguageId { get; set; } = 1;
        public int SignedUserId { get; set; }

        public Expression<Func<UserContentVariantHistory, bool>> CreateFilterExpression()
        {
            Expression<Func<UserContentVariantHistory, bool>> mainExpression = null;

            List<Expression<Func<UserContentVariantHistory, bool>>> predicates =
            [
                p => p.Content.Languages.Any(c => c.LanguageId == LanguageId),
                p => p.UserId == SignedUserId
            ];

            if (CategoryId.HasValue) predicates.Add(p => p.Content.CategoryId == CategoryId);

            predicates?.ForEach(predicate => mainExpression = mainExpression.Append(predicate, ExpressionType.AndAlso));

            return mainExpression;
        }
    }
}
