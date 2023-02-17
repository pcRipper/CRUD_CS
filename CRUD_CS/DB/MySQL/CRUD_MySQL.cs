using CRUD_CS.DB.Entities;
using CRUD_CS.ExpressionReader.MySQL_ER;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace CRUD_CS.DB.MySQL
{
    class CRUD_MySQL : ICRUD<SqlConnection>
    {
        SqlConnection connection;
        MySQL_Translator<Parser_MySQL_Basic> predicateParser;
        MySQL_TranslatorTransformer<Parser_MySQL_Basic> updateParser;
        public SqlConnection SetConnection { set { connection = value; } }
        public bool isConnected { get { return connection != null && connection.State == System.Data.ConnectionState.Open; } }

        public CRUD_MySQL()
        {
            connection = null;
            predicateParser = new MySQL_Translator<Parser_MySQL_Basic>();
            updateParser = new MySQL_TranslatorTransformer<Parser_MySQL_Basic>();
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

                return new KeyValuePair<SqlConnection, Exception>(null, new Exception("connection error!"));
            }
            catch (Exception error)
            {
                return new KeyValuePair<SqlConnection, Exception>(null, error);
            }
        }

        private KeyValuePair<DataTable, Exception> pushQuery(string query)
        {
            if (isConnected)
            {
                try
                {
                    DataSet ds = new DataSet();
                    SqlDataAdapter adapter = new SqlDataAdapter(new SqlCommand(query, connection));
                    adapter.Fill(ds);

                    return new KeyValuePair<DataTable, Exception>(
                        (ds.Tables.Count > 0) ? ds.Tables[0] : null,
                        null
                    );
                }
                catch (Exception error)
                {
                    return new KeyValuePair<DataTable, Exception>(null, error);
                }
            }

            return new KeyValuePair<DataTable, Exception>(null, new Exception("connection does not exist!"));
        }

        public bool Insert<EntityType>(List<EntityType> data) where EntityType : CRUD_Entityt<EntityType>, new()
        {

            if (data != null && data.Count == 0) return false;

            string query = $"INSERT INTO {typeof(EntityType).Name} VALUES ";

            foreach (EntityType row in data)
            {
                query += $"({row.toQuery()}),";
            }

            query = query.Substring(0, query.Length - 1);

            var response = this.pushQuery(query);

            if (response.Key == null) return false;

            return true;
        }

        public List<EntityType> Select<EntityType>(Expression<Predicate<EntityType>> predicate) where EntityType : CRUD_Entityt<EntityType>, new()
        {
            string query = $"SELECT * FROM {typeof(EntityType).Name}";
            string whereClause = predicateParser.Translate(predicate);

            query += (whereClause == "1") ? "" : $" WHERE {whereClause}" ;

            var result = this.pushQuery(query);

            List<EntityType> data = new List<EntityType>();
            EntityType rowParser = new EntityType();

            if (result.Key == null) return data;

            foreach (DataRow row in result.Key.Rows)
            {
                data.Add(rowParser.fromQuery(row));
            }

            return data;
        }

        public bool Update<EntityType>(Expression<Predicate<EntityType>> predicate, Expression<Func<EntityType, EntityType>> transformer)
        {
            string query = $"UPDATE {typeof(EntityType).Name} SET ";
            string setStatement = updateParser.Translate(transformer);
            string whereStatement = predicateParser.Translate(predicate);

            query += $"{setStatement.Substring(0, setStatement.Length - 2)} " +
                $"{((whereStatement == "1") ? "" : "WHERE" + predicateParser.Translate(predicate))}";

            var result = this.pushQuery(query);

            return result.Value == null;
        }

        public bool Remove<EntityType>(Expression<Predicate<EntityType>> predicate)
        {
            string query = $"DELETE FROM {typeof(EntityType).Name} ";
            string whereStatement = predicateParser.Translate(predicate);

            query += (whereStatement == "1") ? "" : $" WHERE {whereStatement}";

            var result = this.pushQuery(query);

            return result.Value == null;
        }
    }
}
