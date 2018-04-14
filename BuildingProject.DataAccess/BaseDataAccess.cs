using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingProject.DataAccess
{
    public class BaseDataAccess<T> : IBaseDataAccess<T> where T : class
    {
        public int Delete(T entity)
        {
            using (var dbContext = new BuildingContext())
            {
                dbContext.Entry(entity).State = EntityState.Deleted;
                return dbContext.SaveChanges();
            }
        }

        public List<T> GetAll()
        {
            using (var dbContext = new BuildingContext())
            {
                return dbContext.Set<T>().ToList();
            }
        }


        public int Insert(T entity)
        {
            using (var dbContext = new BuildingContext())
            {
                dbContext.Entry(entity).State = EntityState.Added;
                return dbContext.SaveChanges();
            }
        }

        public int Update(T entity)
        {
            using (var dbContext = new BuildingContext())
            {
                dbContext.Entry(entity).State = EntityState.Modified;
                return dbContext.SaveChanges();
            }
        }
    }
}
