using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_CS.ExpressionReader.MySQL_ER
{
    class MySQL_Translator<Parser> : MyQueryTranslator
        where Parser : Parser_MySQL,new()
    {
        private Parser parser;

        public MySQL_Translator()
        {
            parser = new Parser();
        }
        protected override Expression VisitConstant(ConstantExpression c)
        {
            switch (Type.GetTypeCode(c.Value.GetType()))
            {
                case TypeCode.DateTime :

                    sb.Append(parser.RepresentDate(Convert.ToDateTime(c.Value)));

                break;
                case TypeCode.String :

                    sb.Append($"'{c.Value.ToString()}'");

                break;
                default :
                    return base.VisitConstant(c);
                break;
            }

            return c;
        }

        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            switch (m.Method.Name)
            {
                case "AddDays" :
                    return parser.AddDays(this, m);
                break;
                case "DATEDIFF" :
                    return parser.DATEDIFF(this, m);
                break;
                default:
                    return base.VisitMethodCall(m);
                break;
            }
        }

        protected override Expression VisitNew(NewExpression node)
        {
            if (node.Constructor == null)
            {
                switch (node.Type.FullName)
                {
                    case "System.DateTime":
                        return Visit(Expression.Constant(new DateTime(1, 1, 1)));
                    break;
                }

                return null;
            }
            else
            {
                return base.VisitNew(node);
            }
        }

        protected override Expression VisitMember(MemberExpression m)
        {
            if (m.Expression == null && m.Type.FullName == "System.DateTime")
            {
                switch (m.Member.Name)
                {
                    case "Now":
                        sb.Append(parser.RepresentDate(DateTime.Now));
                        break;
                }

                return m;
            }
            else
            {
                return base.VisitMember(m);
            }
        }
    }
}
