using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Migrations;

namespace EfficiencyClassWebAPI.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        
        protected DbContext Context { get; set; }

        public Repository()
        {
            
        }
        public Repository(DbContext context)
        {
            Context = context;
        }

        public virtual TEntity Get(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        public virtual IEnumerable<TEntity> GetAll(string include)
        {
            return Context.Set<TEntity>().Include(include).ToList();
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        public virtual IEnumerable<TEntity> FindDeep(Expression<Func<TEntity, bool>> predicate, string include)
        {
            return Context.Set<TEntity>().Include(include).Where(predicate);
        }

        public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().SingleOrDefault(predicate);
        }

        public virtual void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            Context.SaveChanges();
          
        }
       

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
           
            Context.Set<TEntity>().AddRange(entities);
            Context.SaveChanges();
           
        }

        public virtual void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
            Context.Entry(entity).State = EntityState.Deleted;
            Context.SaveChanges();

        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {

            Context.Set<TEntity>().RemoveRange(entities);
            Context.SaveChanges();
        }

        public virtual void Update(TEntity entity, List<string> fields)
        {
            Context.Set<TEntity>().Attach(entity);
            foreach (var field in fields)
            {
                Context.Entry(entity).Property(field).IsModified = true;
            }
            Context.SaveChanges();
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entites, List<string> fields)
        {
            foreach (var entity in entites)
            {
                //Context.Set<TEntity>().AddOrUpdate(entity);
                //Context.SaveChanges();
                Context.Set<TEntity>().Attach(entity);
                foreach (var field in fields)
                {
                    Context.Entry(entity).Property(field).IsModified = true;
                }
                Context.SaveChanges();

            }
        }
        
    }
}