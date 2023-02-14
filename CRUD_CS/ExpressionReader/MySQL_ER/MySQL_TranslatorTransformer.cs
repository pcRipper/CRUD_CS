using System;
using System.Linq.Expressions;

namespace CRUD_CS.ExpressionReader.MySQL_ER
{
    class MySQL_TranslatorTransformer<Parser> : MyQueryTranslatorTransformer
        where Parser : Parser_MySQL, new()
    {
        private Parser parser;

        public MySQL_TranslatorTransformer()
        {
            parser = new Parser();
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
