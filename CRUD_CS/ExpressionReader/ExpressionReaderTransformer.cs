using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CRUD_CS.ExpressionReader
{
    public class MyQueryTranslatorTransformer : MyQueryTranslator
    {
        protected override Expression VisitNew(NewExpression node)
        {
            bool added = false;

            for (int k = 0;k < node.Arguments.Count; k++)
            {
                var argument = node.Arguments[k];
                string name = node.Constructor.GetParameters()[k].Name;

                added = true;

                sb.Append($"{name} = ");
                this.Visit(argument);

                if(added == true)
                {
                    sb.Append(", ");
                    }
            }

            return node;
        }
    }
}
