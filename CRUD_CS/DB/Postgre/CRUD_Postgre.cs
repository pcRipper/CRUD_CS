using CRUD_CS.DB.Entities;
using CRUD_CS.ExpressionReader.Postgre_ER;
using Npgsql;
using OneOf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace CRUD_CS.DB.Postgre
{
    class CRUD_Postgre : ICRUD<NpgsqlConnection>
    {
        NpgsqlConnection connection;
        Postgre_Translator<Parser_Postgre_Basic> predicateParser;
        Postgre_TranslatorTransformer<Parser_Postgre_Basic> updateParser;
        public OneOf<NpgsqlConnection, Exception> SetConnection { set { connection = value.IsT0 ? value.AsT0 : null; } }
        public bool isConnected { get { return connection != null && connection.State == System.Data.ConnectionState.Open; } }

        public CRUD_Postgre()
        {
            connection = null;
            predicateParser = new Postgre_Translator<Parser_Postgre_Basic>();
            updateParser = new Postgre_TranslatorTransformer<Parser_Postgre_Basic>();
        }


        public OneOf<NpgsqlConnection, Exception> connect(Dictionary<string, string> settings)
        {
            try
            {
                NpgsqlConnection connection = new NpgsqlConnection(
                    $"Data Source={     settings["server"] };" +
                    $"Initial Catalog={ settings["db_name"] };" +
                    "Integrated Security=true;"
                );

                connection.Open();

                if (connection.State == ConnectionState.Open)
                {
                    return connection;
                }

                return new Exception("connection error!");
            }
            catch (Exception error)
            {
                return error;
            }
        }

        private KeyValuePair<DataTable, Exception> pushQuery(string query)
        {
            if (isConnected)
            {
                try
                {
                    DataSet ds = new DataSet();
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(new NpgsqlCommand(query, connection));
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
            string query = $"SELECT * FROM {typeof(EntityType).Name} WHERE " + predicateParser.Translate(predicate);

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

            query += (whereStatement == "1") ? "" : whereStatement;

            var result = this.pushQuery(query);

            return result.Value == null;
        }
    }
}
