using RestaurantTableReservation.DataAccessLayer.Entity;
using RestaurantTableReservation.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantTableReservation.BusinessAccessLayer
{
    public interface IAdminService
    {
        Task<IList<User>> AllPendingCustumer();
        Task<IList<User>> AllCustomers();
        Task<List<ReservationOverviewDto>> GetAllReservations();
        Task<AccountResponseDto> ChangeStatus(string userId, bool userStatus);
        Task CreateTable(ModalTableDto tableDto);
        Task UpdateTable(ModalTableDto tableDto);
        Task DeleteTable(Guid id);
        Task<ModalTableDto> GetById(Guid id);
        Task<List<TableDetailDto>> GetAllTableDetails(); 
    }
}
