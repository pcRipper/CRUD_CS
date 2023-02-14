using System;
using System.Linq.Expressions;

namespace CRUD_CS.ExpressionReader.MySQL_ER
{
    //To do :
    // DataFunctions : 
    //  ADDDATE -> implemented only for days adding
    //  CURDATE -> is not necessarily,beacause can be taken as a constant from C#
    //  DATE
    //  DATEDIFF -> impelemented as DATEDIFF in MySQL_FEX, return type int (as difference in days)
    //   

    public interface Parser_MySQL
    {
        public Expression StringLength(MyQueryTranslator translator, MemberExpression member);
        public Expression AddDays(MyQueryTranslator translator, MethodCallExpression method);
        public Expression DATEDIFF(MyQueryTranslator translator, MethodCallExpression method);
        public string RepresentDate(DateTime date);

    }

    public class Parser_MySQL_Basic : Parser_MySQL
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
