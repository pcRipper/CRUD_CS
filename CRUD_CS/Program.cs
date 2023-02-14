using CRUD_CS.DB.Entities;
using CRUD_CS.DB.MySQL;
using CRUD_CS.ExpressionReader.MySQL_ER;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CRUD_CS
{
    class Program
    {
        static void translatorTests()
        {
            int k = 0;

            MySQL_Translator<Parser_MySQL_Basic> translator = new MySQL_Translator<Parser_MySQL_Basic>();
            MySQL_TranslatorTransformer<Parser_MySQL_Basic> translator_transform = new MySQL_TranslatorTransformer<Parser_MySQL_Basic>();

            Expression<Func<_User, _User>> act = x => new _User(x._email, x._password, x._name + "_hello", x._surname, x._dob, x._sallary * 2 * 2);

            //Console.WriteLine(translator.Translate());
            Console.WriteLine(translator_transform.Translate(act));
        }
        static void Main(string[] args)
        {
            CRUD_MySQL crud = new CRUD_MySQL();

            crud.SetConnection = crud.connect(new Dictionary<string, string>() {
                { "db_name", "ZP_APSL" },
                { "server",  "DESKTOP-AJ9MC4H" }
            }).Key;

            Console.WriteLine(crud.isConnected);

            //List<_User> result = crud.Select<_User>((x) => x._sallary > 50000 && x._surname.Length < 5);

            //if (result != null)
            //{
            //    foreach (_User user in result) Console.WriteLine(user);
            //}

            //crud.Update<_User>(x => true, x => new _User(x._email, x._password, x._name, x._surname, x._dob, x._sallary * 2));

            crud.Update<_User>(x => true, x => new _User(x._email, x._password, x._name + "_hello", x._surname, x._dob, x._sallary * 2 * 2));

            //translatorTests();


        }
    }
}
