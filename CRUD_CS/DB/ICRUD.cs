using System;
using System.Data;
using System.Linq.Expressions;
using System.Collections.Generic;

using CRUD_CS.DB.Entities;

namespace CRUD_CS.DB
{
    public interface ICRUD<ConnectionType>
    {
        KeyValuePair<ConnectionType,Exception> connect(Dictionary<string, string> settings);

        List<EntityType> Select<EntityType>(Predicate<EntityType> predicate) where EntityType : CRUD_Entityt<EntityType>, new();

        bool Insert<EntityType>(List<EntityType> data) where EntityType : CRUD_Entityt<EntityType>, new();

        bool Update<EntityType>(Predicate<EntityType> predicate, Action<EntityType> transformer);

        bool Remove<EntityType>(Predicate<EntityType> predicate);
    }
}
