using System.Linq.Expressions;

namespace ExpressionTransformation
{
    public class IncDecReplacer : ExpressionVisitor
    {
        protected override Expression VisitBinary(BinaryExpression node)
        {
            Expression paramNode = null;
            if (node.NodeType == ExpressionType.Add && IsParamAndOne(node, ref paramNode))
            {
                return Expression.Increment(paramNode);
            }
            if (node.NodeType == ExpressionType.Subtract && IsParamAndOne(node, ref paramNode))
            {
                return Expression.Decrement(paramNode);
            }

            return base.VisitBinary(node);
        }

        private bool IsParamAndOne(BinaryExpression node, ref Expression paramNode)
        {
            ConstantExpression constNode = null;

            if (node.Left.NodeType == ExpressionType.Constant && node.Right.NodeType == ExpressionType.Parameter)
            {
                constNode = (ConstantExpression)node.Left;
                paramNode = node.Right;
            }
            else if (node.Left.NodeType == ExpressionType.Parameter && node.Right.NodeType == ExpressionType.Constant)
            {
                constNode = (ConstantExpression)node.Right;
                paramNode = node.Left;
            }

            int val;
            return constNode != null
                && constNode.Type != typeof(string)
                && int.TryParse(constNode.Value.ToString(), out val)
                && val == 1;
        }
    }
}
