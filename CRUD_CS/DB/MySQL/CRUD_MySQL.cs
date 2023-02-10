using CRUD_CS.DB.Entities;
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
        SqlConnection connection;
        
        public CRUD_MySQL()
        {
            
        }

        public KeyValuePair<SqlConnection, Exception> connect(Dictionary<string, string> settings)
        {
            try
            {
                SqlConnection connection = new SqlConnection(
                    $"Data Source={     settings["server"] };" +
                    $"Initial Catalog={ settings["db_name"] };" +
                    "Integrated Security=true;"
                );

                connection.Open();

                if (connection.State == ConnectionState.Open)
                {
                    return new KeyValuePair<SqlConnection, Exception>(connection, null);
                }

                return new KeyValuePair<SqlConnection, Exception>(null,new Exception("connection error!"));
            }
            catch (Exception error)
            {
                return new KeyValuePair<SqlConnection, Exception>(null, error);
            }
        }

        public bool Insert<EntityType>(List<EntityType> data) where EntityType : CRUD_Entityt<EntityType>, new()
        {
            throw new NotImplementedException();
        }

        public bool Remove<EntityType>(Predicate<EntityType> predicate)
        {
            throw new NotImplementedException();
        }

        public List<EntityType> Select<EntityType>(Predicate<EntityType> predicate) where EntityType : CRUD_Entityt<EntityType>, new()
        {
            throw new NotImplementedException();
        }

        public bool Update<EntityType>(Predicate<EntityType> predicate, Action<EntityType> transformer)
        {
            throw new NotImplementedException();
        }
    }
}
