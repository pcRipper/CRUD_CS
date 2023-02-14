using System;
using System.Linq.Expressions;

namespace CRUD_CS.ExpressionReader.Postgre_ER
{
    public interface Parser_Postgre
    {
        public Expression StringLength(MyQueryTranslator translator, MemberExpression member);
        public Expression AddDays(MyQueryTranslator translator, MethodCallExpression method);
        public Expression DATEDIFF(MyQueryTranslator translator, MethodCallExpression method);
        public string RepresentDate(DateTime date);

    }

    public class Parser_Postgre_Basic : Parser_Postgre
    {
        public Expression AddDays(MyQueryTranslator translator, MethodCallExpression method)
        {
            translator.QueryString = "ADDDATE(";
            translator.Visit(method.Object);
            translator.QueryString = ",";
            translator.Visit(method.Arguments[0]);
            translator.QueryString = ")";

            return null;
        }

        public Expression DATEDIFF(MyQueryTranslator translator, MethodCallExpression method)
        {

            translator.QueryString = "DATEDIFF(";
            translator.Visit(method.Arguments[0]);
            translator.QueryString = ",";
            translator.Visit(method.Arguments[1]);
            translator.QueryString = ")";

            return null;
        }

        public string RepresentDate(DateTime date)
        {
            return $"'{date.ToString("dd-MM-yyyy")}'";
        }

        public Expression StringLength(MyQueryTranslator translator, MemberExpression member)
        {
            translator.QueryString = "LEN(";
            translator.Visit(member.Expression);
            translator.QueryString = ")";

            return member;
        }
    }
}
