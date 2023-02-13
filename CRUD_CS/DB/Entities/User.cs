using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_CS.DB.Entities
{
    class User
    {
        public string _email { get; set; }
        public string _password { get; set; }
        public string _name { get; set; }
        public string _surname { get; set; }
        public int age { get; set; }
        public DateTime dob { get; set; }
    }
}
