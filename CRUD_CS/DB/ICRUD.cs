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
        bool Insert<EntityType>(List<EntityType> data) where EntityType : CRUD_Entityt<EntityType>, new();
        List<EntityType> Select<EntityType>(Expression<Predicate<EntityType>> predicate) where EntityType : CRUD_Entityt<EntityType>, new();
        bool Update<EntityType>(Expression<Predicate<EntityType>> predicate, Expression<Func<EntityType,EntityType>> transformer);

        bool Remove<EntityType>(Expression<Predicate<EntityType>> predicate);
    }
}
