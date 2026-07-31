using RestaurantTableReservation.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantTableReservation.BusinessAccessLayer
{
    public interface ICustomerService
    {
        Task<object> GetPaginatedProducts(int pageNumber, int pageSize);
        Task Create(ModalReservationDto reservationDto);
        Task Update(ModalReservationDto reservationDto);
        Task Delete(Guid id);
        Task<object> GetReservation(string? customerId = null);
        Task<ModalReservationDto> GetReservationById(Guid id);
    }
}
