using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_CS.DB.MySQL
{
    class CRUD_MySQL : ICRUD<SqlConnection>
    {
        public SqlConnection connect(Dictionary<string, string> settings)
        {
            throw new NotImplementedException();
        }

        public bool Insert<EntityType>(List<EntityType> data)
        {
            throw new NotImplementedException();
        }

        public bool Remove<EntityType>(Func<EntityType, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public DataTable Select<EntityType>(Func<EntityType, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public bool Update<EntityType>(Func<EntityType, bool> predicate, Func<EntityType, EntityType> transformer)
        {
            throw new NotImplementedException();
        }
    }
}
