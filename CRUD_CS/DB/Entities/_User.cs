using System;
using System.Data;

namespace CRUD_CS.DB.Entities
{
    class _User : CRUD_Entityt<_User>
    {
        public _User()
        {

        }
        public _User(string _email, string _password, string _name, string _surname, DateTime _dob, double _sallary)
        {
            this._email = _email;
            this._password = _password;
            this._name = _name;
            this._surname = _surname;
            this._dob = _dob;
            this._sallary = _sallary;
        }
        public string _email { get; set; }
        public string _password { get; set; }
        public string _name { get; set; }
        public string _surname { get; set; }
        public DateTime _dob { get; set; }
        public double _sallary { get; set; }

        public _User fromQuery(DataRow toParse)
        {
            return new _User(
                toParse[0] as string,
                toParse[1] as string,
                toParse[2] as string,
                toParse[3] as string,
                Convert.ToDateTime(toParse[4]),
                Convert.ToDouble(toParse[5])
            );
        }

        public string toQuery()
        {
            return $"'{_email}','{_password}','{_name}','{_surname}','{_dob}',{_sallary}";
        }

        public override string ToString()
        {
            return $"{_email},{_password},{_name},{_surname},{_dob},{_sallary}";
        }
    }
}
