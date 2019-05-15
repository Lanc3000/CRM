using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using CRMCore.DB.Identity;
using CRMCore.Repositories;
using CRMCore.DB;

namespace CRMCore.Repositories.Impl
{
    public class Repository<T> : IRepository<T>
        where T : class, IEntity
    {
        DBContext database;
        public Repository(DBContext context)
        {
            database = context;
        }

        protected IQueryable<T> Queryable()
        {
            return database.Set<T>()
                .Where(x => !x.Deleted);
        }

        protected IQueryable<T> QueryableWithDeleted()
        {
            return database.Set<T>();
        }

        protected IQueryable<TOther> Queryable<TOther>()
            where TOther : class, IEntity
        {
            return database.Set<TOther>()
                .Where(x => !x.Deleted);
        }

        protected IQueryable<TOther> QueryableWithDeleted<TOther>()
            where TOther : class, IEntity
        {
            return database.Set<TOther>();
        }

        public virtual IEnumerable<T> All()
        {
            return Queryable().OrderByDescending(x => x.Id).ToList();
        }

        public virtual IEnumerable<T> AllWithDeleted()
        {
            return QueryableWithDeleted().ToList();
        }

        public virtual T Get(long key)
        {
            return database.Set<T>().SingleOrDefault(x => x.Id == key);
        }

        public virtual T Get (int key)
        {
            return database.Set<T>().SingleOrDefault(x => x.Id == key);
        }

        public virtual void Insert(T entity)
        {
            entity.Created = DateTime.Now;
            entity.Modified = entity.Created;
            entity.Deleted = false;
            database.Set<T>().Add(entity);
        }

        public virtual void Update(T entity)
        {
            entity.Modified = DateTime.Now;
            database.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            entity.Deleted = true;
        }

        public void Delete(long id)
        {
            var entity = Get(id);
            entity.Deleted = true;
            entity.Modified = DateTime.Now;
            Update(entity);
        }

        public void SaveChanges()
        {
            database.SaveChanges();
        }

        //protected IQueryable<T> TextSearch(string tableName, string query)
        //{
        //    return DbContext.Set<T>().FromSql($@"
        //            select *, ts_rank(""Tsvector"", to_tsquery(regexp_replace(cast(plainto_tsquery({{0}}) as text), E'(\'\\w+\')', E'\\1:*', 'g'))) AS rank 
        //            from ""{tableName}""
        //            where ""Tsvector"" @@ to_tsquery(regexp_replace(cast(plainto_tsquery({{0}}) as text), E'(\'\\w+\')', E'\\1:*', 'g')) AND ""Deleted"" = 'false'
        //            ORDER BY rank DESC
        //        ", query);
        //}
    }
}