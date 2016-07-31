using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ExpressionTransformation
{
    static class ExpressionTransformer
    {
        public static Expression<T> ReplaceIncDec<T>(Expression<T> expression)
        {
            var replacer = new IncDecReplacer();
            return replacer.VisitAndConvert(expression, nameof(ExpressionTransformer));
        }

        public static LambdaExpression ReplaceParams<T>(Expression<T> expression, Dictionary<string, object> map)
        {
            var leftParams = expression.Parameters.Where(param => !map.ContainsKey(param.Name));
            var replacer = new ParamsReplacer(map);
            var body = replacer.VisitAndConvert(expression.Body, nameof(ReplaceParams));

            return Expression.Lambda(body, leftParams);
        }
    }
}
