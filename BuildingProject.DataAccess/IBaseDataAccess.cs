using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingProject.DataAccess
{
    public interface IBaseDataAccess<T>
    {
        List<T> GetAll();
        int Insert(T entity);
        int Delete(T entity);
        int Update(T entity);
    }
}
