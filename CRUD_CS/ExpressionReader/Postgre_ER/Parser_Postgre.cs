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

            translator.QueryString = "DATE_PART('day',";
            translator.Visit(method.Arguments[0]);
            translator.QueryString = "::timestamp - ";
            translator.Visit(method.Arguments[1]);
            translator.QueryString = "::timestamp)";

            return null;
        }

        public string RepresentDate(DateTime date)
        {
            return $"'{date.ToString("yyyy-MM-dd")}'";
        }

        public Expression StringLength(MyQueryTranslator translator, MemberExpression member)
        {
            translator.QueryString = "Length(";
            translator.Visit(member.Expression);
            translator.QueryString = ")";

            return member;
        }
    }
}
