using System;
using System.Linq.Expressions;

using CRUD_CS.DB.TypesFunctionalExtensions;
using CRUD_CS.DB.Entities;
using CRUD_CS.ExpressionReader;
using CRUD_CS.ExpressionReader.MySQL_ER;

namespace CRUD_CS
{
    class Program
    {
        static void Main(string[] args)
        {
            int k = 0;

            MySQL_Translator<Parser_MySQL_Basic> translator = new MySQL_Translator<Parser_MySQL_Basic>();
            MySQL_TranslatorTransformer<Parser_MySQL_Basic> translator_transform = new MySQL_TranslatorTransformer<Parser_MySQL_Basic>();

            Expression<Predicate<User>> exp = (x) =>  x._name.Length < 5 && x._dob > new DateTime(2002,12,1);
            Expression<Predicate<User>> exp2 = (x) => new DateTime(2005, 12, 2) > new DateTime(2002, 12, 1).AddDays(2) && new int?(12) > 2;
            Expression<Func<User, User>> act = x => new User(x._email,"12", x._name, x._surname, x._age / 2, DateTime.Now);
            Expression<Predicate<User>> exp3 = x => x._dob.DATEDIFF(DateTime.Now) > 12;


            Console.WriteLine(translator.Translate(exp3));
            Console.WriteLine(translator_transform.Translate(act));

        }
    }
}
