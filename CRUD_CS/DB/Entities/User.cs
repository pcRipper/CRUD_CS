using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_CS.DB.Entities
{
    class User
    {
        public User(string email, string password, string name, string surname, int age, DateTime dob)
        {
            _email = email;
            _password = password;
            _name = name;
            _surname = surname;
            this.age = age;
            this.dob = dob;
        }
        public string _email { get; set; }
        public string _password { get; set; }
        public string _name { get; set; }
        public string _surname { get; set; }
        public int age { get; set; }
        public DateTime dob { get; set; }
    }
}
