using System.Collections.Generic;

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
