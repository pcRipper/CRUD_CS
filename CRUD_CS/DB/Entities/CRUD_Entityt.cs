using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_CS.DB.Entities
{
    public interface CRUD_Entityt<Type> where Type : new()
    {
        public string toQuery();
        public Type fromQuery(DataRow toParse);
    }
}
