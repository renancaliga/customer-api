using CustomerAPI.Domain.Entities;
using CustomerAPI.Domain.Interfaces;
using CustomerAPI.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAPI.Infra.Data.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity
        : BaseEntity
    {
        protected readonly SqlContext _sqlContext;

        public BaseRepository(SqlContext sqlContext)
        {
            _sqlContext = sqlContext;
        }

        public void Insert(TEntity obj)
        {
            _sqlContext.Set<TEntity>().Add(obj);
            _sqlContext.SaveChanges();
        }

        public void Update(TEntity obj)
        {
            _sqlContext.Entry(obj).State =
                Microsoft.EntityFrameworkCore.EntityState.Modified;
            _sqlContext.SaveChanges();
        }

        public void Delete(int id)
        {
            _sqlContext.Set<TEntity>().Remove(Select(id));
            _sqlContext.SaveChanges();

        }

        public IList<TEntity> Select() =>
            _sqlContext.Set<TEntity>().ToList();

        public TEntity Select(int id) =>
            _sqlContext.Set<TEntity>().Find(id);

    }
}
