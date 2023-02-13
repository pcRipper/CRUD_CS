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
                default:
                    return base.VisitMethodCall(m);
                break;
            }
        }
    }
}
