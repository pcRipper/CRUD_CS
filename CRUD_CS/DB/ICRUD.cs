using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_CS.DB
{
    public interface ICRUD<ConnectionType>
    {
        ConnectionType connect(Dictionary<string, string> settings);

        DataTable Select<EntityType>(Func<EntityType, bool> predicate);

        bool Insert<EntityType>(List<EntityType> data);

        bool Update<EntityType>(Func<EntityType, bool> predicate,Func<EntityType,EntityType> transformer);

        bool Remove<EntityType>(Func<EntityType, bool> predicate);
    }
}
