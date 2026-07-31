using RestaurantTableReservation.DataAccessLayer.Entity;
using RestaurantTableReservation.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantTableReservation.BusinessAccessLayer
{
    public interface IAccountService
    {
        Task<AccountResponseDto> Register(RegisterDto register);
        Task<AccountResponseDto> Login(LoginDto login);
        Task<AccountResponseDto> Logout();
        
      
    }
}
