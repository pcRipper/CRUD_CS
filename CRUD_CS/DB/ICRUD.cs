using CRUD_CS.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CRUD_CS.DB
{
    public interface ICRUD<ConnectionType>
    {
        KeyValuePair<ConnectionType, Exception> connect(Dictionary<string, string> settings);
        bool Insert<EntityType>(List<EntityType> data) where EntityType : CRUD_Entityt<EntityType>, new();
        List<EntityType> Select<EntityType>(Expression<Predicate<EntityType>> predicate) where EntityType : CRUD_Entityt<EntityType>, new();
        bool Update<EntityType>(Expression<Predicate<EntityType>> predicate, Expression<Func<EntityType, EntityType>> transformer);

        bool Remove<EntityType>(Expression<Predicate<EntityType>> predicate);
    }
}
