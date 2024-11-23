using System.Linq.Expressions;
using System.Reflection;

namespace Cms.Common.Helpers
{
    public static class ExpressionHelpers
    {
        public static Expression<Func<T, bool>> Append<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right, ExpressionType expressionAppendType)
        {
            if (right != null)
            {
                Expression<Func<T, bool>> expression = null;
                switch (expressionAppendType)
                {
                    case ExpressionType.OrElse:
                        if (left == null)
                        {
                            left = (T model) => false;
                        }

                        return left.OrElse(right);
                    case ExpressionType.AndAlso:
                        if (left == null)
                        {
                            left = (T model) => true;
                        }

                        return left.AndAlso(right);
                    default:
                        throw new InvalidOperationException();
                }
            }

            return left;
        }

        public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left.Body, new ExpressionParameterReplacer(right.Parameters, left.Parameters).Visit(right.Body)), left.Parameters);
        }

        public static Expression<Func<T, bool>> OrElse<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(left.Body, new ExpressionParameterReplacer(right.Parameters, left.Parameters).Visit(right.Body)), left.Parameters);
        }

        public static MemberInfo GetPropertyInfo<T, TPropertyType>(this Expression<Func<T, TPropertyType>> expression)
        {
            if (expression.Body is MemberExpression)
            {
                return ((MemberExpression)expression.Body).Member;
            }

            return ((MemberExpression)((UnaryExpression)expression.Body).Operand).Member;
        }

        public static string GetPropertyName<T, TPropertyType>(this Expression<Func<T, TPropertyType>> expression)
        {
            if (expression.Body is MemberExpression)
            {
                return ((MemberExpression)expression.Body).Member.Name;
            }

            return ((MemberExpression)((UnaryExpression)expression.Body).Operand).Member.Name;
        }
    }

    public class ExpressionParameterReplacer : ExpressionVisitor
    {
        private IDictionary<ParameterExpression, ParameterExpression> ParameterReplacements { get; set; }

        public ExpressionParameterReplacer(IList<ParameterExpression> fromParameters, IList<ParameterExpression> toParameters)
        {
            ParameterReplacements = new Dictionary<ParameterExpression, ParameterExpression>();
            for (int i = 0; i != fromParameters.Count && i != toParameters.Count; i++)
            {
                ParameterReplacements.Add(fromParameters[i], toParameters[i]);
            }
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (ParameterReplacements.TryGetValue(node, out var value))
            {
                node = value;
            }

            return base.VisitParameter(node);
        }
    }
}
