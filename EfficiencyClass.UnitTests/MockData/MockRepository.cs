using EfficiencyClassWebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EfficiencyClass.UnitTests.MockData
{
    public class MockRepository<TEntity>: IRepository<TEntity> where TEntity : class
    {
        //MockDataContext Context { get; set; }

        public List<TEntity> Context;

        public MockRepository(List<TEntity> context)
        {
            Context = context;
        }

        public virtual TEntity Get(int id)
        {
            return Context.SingleOrDefault();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return Context.ToList();
        }

        public virtual IEnumerable<TEntity> GetAll(string include)
        {
            return Context.ToList();
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.AsQueryable().Where(predicate);
        }

        public virtual IEnumerable<TEntity> FindDeep(Expression<Func<TEntity, bool>> predicate, string include)
        {
            return Context.AsQueryable().Where(predicate);
        }

        public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.AsQueryable().SingleOrDefault(predicate);
        }

        public virtual void Add(TEntity entity)
        {
            Context.Add(entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            Context.AddRange(entities);
        }

        public virtual void Remove(TEntity entity)
        {
            Context.Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Context.Remove(entity);
            }
        }

        public virtual void Update(TEntity entity, List<string> fields)
        {
            var entry = Context.Where(s => s == entity).SingleOrDefault();
            entry = entity;
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entites, List<string> fields)
        {
            foreach (var entity in entites)
            {
                var entry = Context.Where(s => s == entity).SingleOrDefault();
                entry = entity;
            }
        }
    }
}
