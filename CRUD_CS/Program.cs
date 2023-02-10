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

            MyQueryTranslator translator = new MyQueryTranslator();

            Expression<Predicate<User>> exp = (x) => x.age.FLOOR() > 12.5 && x._name.Length < 5;

            Console.WriteLine(translator.Translate(exp));

        }
    }
}
