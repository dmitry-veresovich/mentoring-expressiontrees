using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionTransformation
{
    class ParamsReplacer : ExpressionVisitor
    {
        private readonly Dictionary<string, object> _map;

        public ParamsReplacer(Dictionary<string, object> map)
        {
            _map = map;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (_map.ContainsKey(node.Name))
            {
                var value = _map[node.Name];
                return Expression.Constant(value);
            }

            return base.VisitParameter(node);
        }
    }
}
