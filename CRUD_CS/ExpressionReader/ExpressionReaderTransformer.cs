using System.Linq.Expressions;

namespace CRUD_CS.ExpressionReader
{
    public class MyQueryTranslatorTransformer : MyQueryTranslator
    {
        protected override Expression VisitNew(NewExpression node)
        {
            for (int k = 0; k < node.Arguments.Count; k++)
            {
                var argument = node.Arguments[k];
                string name = node.Constructor.GetParameters()[k].Name;

                sb.Append($"{name} = ");
                this.Visit(argument);

                sb.Append(", ");
            }

            return node;
        }
    }
}
