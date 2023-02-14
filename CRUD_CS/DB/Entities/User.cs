using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_CS.DB.Entities
{
    class User
    {
        public User(string _email, string _password, string _name, string _surname, int _age, DateTime _dob)
        {
            this._email = _email;
            this._password = _password;
            this._name = _name;
            this._surname = _surname;
            this._age = _age;
            this._dob = _dob;
        }
        public string _email { get; set; }
        public string _password { get; set; }
        public string _name { get; set; }
        public string _surname { get; set; }
        public int _age { get; set; }
        public DateTime _dob { get; set; }
    }
}
