using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestaurantTableReservation.DataAccessLayer;
using RestaurantTableReservation.DataAccessLayer.Entity;
using RestaurantTableReservation.DataTransferObject;
using static RestaurantTableReservation.DataAccessLayer.Repository.IGenericRepository;

namespace RestaurantTableReservation.BusinessAccessLayer
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<User> _userManager;
        private readonly IGenericRepository<TableDetail> _genericRepository;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AdminService(
            UserManager<User> userManager,
            IGenericRepository<TableDetail> genericRepository,
            ApplicationDbContext context,
            IMapper mapper)
        {
            _userManager = userManager;
            _genericRepository = genericRepository;
            _context = context;
            _mapper = mapper;
        }

        public async Task<IList<User>> AllPendingCustumer()
        {
            return await AllCustomers();
        }

        public async Task<IList<User>> AllCustomers()
        {
            var users = await _userManager.GetUsersInRoleAsync("customer");
            return users.OrderByDescending(x => x.CreatedDate).ToList();
        }

        public async Task<List<ReservationOverviewDto>> GetAllReservations()
        {
            var reservations = await _context.ReservationDetails
                .Include(x => x.Table)
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.CreateAt)
                .ToListAsync();

            var result = new List<ReservationOverviewDto>();
            foreach (var r in reservations)
            {
                var customer = await _userManager.FindByIdAsync(r.CustomerId);
                result.Add(new ReservationOverviewDto
                {
                    Id = r.Id,
                    CustomerName = customer?.Name ?? "Unknown",
                    TableNumber = r.Table?.TableNumber ?? 0,
                    NumberOfGuest = r.NumberOfGuest,
                    ReservationDate = r.ReservationDate,
                    StartTime = r.StartTime,
                    EndTime = r.EndTime,
                    Status = r.ReservationStatus == ReservationStatus.Cancelled ? "Cancelled" : "Booked",
                    CreateAt = r.CreateAt
                });
            }
            return result;
        }

        public async Task<AccountResponseDto> ChangeStatus(string userId, bool userStatus)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var response = new AccountResponseDto();
            if (user != null)
            {
                if (userStatus)
                {
                    user.Status = UserStatus.Approved;
                    await _userManager.UpdateAsync(user);
                    response.Status = true;
                    response.Message = "User is Approved";
                    return response;
                }
                user.Status = UserStatus.Rejected;
                await _userManager.UpdateAsync(user);
                response.Status = true;
                response.Message = "User is rejected";
                return response;
            }
            response.Status = false;
            response.Message = "User id not Exist";
            return response;
        }

        public async Task CreateTable(ModalTableDto tableDto)
        {
            var data = _mapper.Map<TableDetail>(tableDto);
            data.CreatedAt = DateTime.Now;
            var datas = await _genericRepository.GetAll();
            data.TableNumber = datas.Count() + 1;
            await _genericRepository.Insert(data);
            await _genericRepository.Save();
        }

        public async Task UpdateTable(ModalTableDto tableDto)
        {
            var data = _mapper.Map<TableDetail>(tableDto);
            await _genericRepository.Update(data);
            await _genericRepository.Save();
        }

        public async Task DeleteTable(Guid id)
        {
           var data = await _genericRepository.GetById(id);
            data.IsDeleted = true;
            await _genericRepository.Update(data);
            await _genericRepository.Save();
        }

        public async Task<List<TableDetailDto>> GetAllTableDetails()
        {
            var data = await _genericRepository.GetAll();
            return _mapper.Map<List<TableDetailDto>>(data.Where(x => !x.IsDeleted).ToList());
        }

        public async Task<ModalTableDto> GetById(Guid id)
        {
            return _mapper.Map<ModalTableDto>(await _genericRepository.GetById(id));
        }
    }
}
