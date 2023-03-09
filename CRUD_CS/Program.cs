using CRUD_CS.DB.Entities;
using CRUD_CS.DB.MySQL;
using System;
using System.Collections.Generic;

namespace CRUD_CS
{
    public static class MethodExtensions
    {
        public static void Show<Container>(this IEnumerable<Container> container,string begin,string separator,string end)
        {
            Console.Write(begin);

            foreach(var k in container)
            {
                Console.Write(k + separator);
            }

            Console.Write(end);
        }

        public static void ShowColumns<Type>(this List<Type> list)
        {
            list.Show($"{list.Count} :\n  ", "\n   ", "\b;\n");
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            CRUD_MySQL crud = new CRUD_MySQL();

            crud.SetConnection = crud.connect(new Dictionary<string, string>() {
                { "db_name", "ZP_APSL" },
                { "server",  "DESKTOP-AJ9MC4H" }
            });

            Console.WriteLine(crud.isConnected);

            var response1 = crud.Select<_User>(x => true);
            var response2 = crud.Update<_User>(
                x => x._sallary > 75000,
                x => new _User(x._email,x._password,x._surname,x._name,x._dob,x._sallary * 4)
            );
            var response3 = crud.Select<_User>(x => true);
            var response4 = crud.Select<_User>(x => x._surname.Length > 12 && x._sallary < 100000);
            var response5 = crud.Remove<_User>(x => x._name.Length > 8);
            var response6 = crud.Select<_User>(x => true);

            response1.ShowColumns();
            response3.ShowColumns();
            response4.ShowColumns();
            response6.ShowColumns();

        }
    }
}
