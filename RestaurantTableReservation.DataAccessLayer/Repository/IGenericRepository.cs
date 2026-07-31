using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantTableReservation.DataAccessLayer.Repository
{
    public interface IGenericRepository
    {
        public interface IGenericRepository<T> where T : class
        {
            Task<IEnumerable<T>> GetAll();
            Task<T> GetById(object id);
            Task Insert(T obj);
            Task Update(T obj);
            Task Delete(object id);
            Task Save();
        }
    }
}
