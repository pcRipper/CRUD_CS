using System;
using System.Linq.Expressions;

namespace CRUD_CS.ExpressionReader.Postgre_ER
{
    class Postgre_TranslatorTransformer<Parser> : MyQueryTranslatorTransformer
            where Parser : Parser_Postgre, new()
    {
        private Parser parser;

        public Postgre_TranslatorTransformer()
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
