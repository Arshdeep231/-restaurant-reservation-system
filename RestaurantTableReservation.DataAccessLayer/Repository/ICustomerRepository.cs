using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantTableReservation.DataAccessLayer.Repository
{
    public interface ICustomerRepository
    {
        Task<object> GetPaginatedProducts(int pageNumber, int pageSize);
        Task<object> GetAllReservation(string? customerId = null);
    }
}
