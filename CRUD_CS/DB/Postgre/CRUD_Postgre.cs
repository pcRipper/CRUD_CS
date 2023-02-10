using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_CS.DB.Postgre
{
    class CRUD_Postgre : ICRUD
    {
        public NpgsqlConnection connect<NpgsqlConnection>(Dictionary<string, string> settings)
        {
            throw new NotImplementedException();
        }

        public bool Insert<EntityType>(List<EntityType> data)
        {
            throw new NotImplementedException();
        }

        public bool Remove<EntityType>(Predicate<EntityType> predicate)
        {
            throw new NotImplementedException();
        }

        public DataTable Select<EntityType>(Predicate<EntityType> predicate)
        {
            throw new NotImplementedException();
        }

        public bool Update<EntityType>(Predicate<EntityType> predicate, Action<EntityType> transformer)
        {
            throw new NotImplementedException();
        }
    }
}
