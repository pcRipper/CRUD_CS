using System.Data;

namespace CRUD_CS.DB.Entities
{
    public interface CRUD_Entityt<Type> where Type : new()
    {
        public string toQuery();
        public Type fromQuery(DataRow toParse);
    }
}
