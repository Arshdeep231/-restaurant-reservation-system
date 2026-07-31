using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantTableReservation.DataAccessLayer.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<object> GetAllReservation(string? customerId = null)
        {
            var query = _context.ReservationDetails
                .Include(x => x.Table)
                .Where(x => !x.IsDeleted);

            if (!string.IsNullOrEmpty(customerId))
            {
                query = query.Where(x => x.CustomerId == customerId);
            }

            return await query
                .OrderByDescending(x => x.CreateAt)
                .ToListAsync();
        }

        public async Task<object> GetPaginatedProducts(int pageNumber, int pageSize)
        {
            var totalRecords = await _context.TableDetails.Where(x => !x.IsDeleted).CountAsync();
            var product = await _context.TableDetails.Where(x => !x.IsDeleted).Skip((pageNumber - 1) * pageSize).Take(pageSize).Cast<object>().ToListAsync();
            var response = new
            {
                TotalRecord = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = product
            };
            return response;
        }
    }
}
