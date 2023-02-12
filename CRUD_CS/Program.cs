using System;
using System.Linq.Expressions;

using CRUD_CS.DB.TypesFunctionalExtensions;
using CRUD_CS.DB.Entities;
using CRUD_CS.ExpressionReader;

namespace CRUD_CS
{
    class Program
    {
        static void Main(string[] args)
        {
            int k = 0;

            DateTime date = DateTime.Now;

            MyQueryTranslator<Parser_MySQL> translator = new MyQueryTranslator<Parser_MySQL>();

            Expression<Predicate<User>> exp = (x) =>  x._name.Length < 5 && x.dob > new DateTime(2002,12,1);
            Expression<Predicate<User>> exp2 = (x) => new DateTime(2005, 12, 2) > new DateTime(2002, 12, 1).AddDays(2) && new int?(12) > 2;

            Console.WriteLine(translator.Translate(exp2));

        }
    }
}
