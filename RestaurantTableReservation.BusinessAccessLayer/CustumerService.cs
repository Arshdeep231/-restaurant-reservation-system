using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantTableReservation.DataAccessLayer;
using RestaurantTableReservation.DataAccessLayer.Entity;
using RestaurantTableReservation.DataAccessLayer.Repository;
using RestaurantTableReservation.DataTransferObject;
using static RestaurantTableReservation.DataAccessLayer.Repository.IGenericRepository;

namespace RestaurantTableReservation.BusinessAccessLayer
{
    public class CustumerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IGenericRepository<ReservationDetail> _repository;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CustumerService(
            ICustomerRepository customerRepository,
            IMapper mapper,
            IGenericRepository<ReservationDetail> repository,
            ApplicationDbContext context)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _repository = repository;
            _context = context;
        }

        public async Task Create(ModalReservationDto reservationDto)
        {
            var entity = _mapper.Map<ReservationDetail>(reservationDto);
            entity.CreateAt = DateTime.Now;
            entity.ReservationStatus = ReservationStatus.Booking;
            await _repository.Insert(entity);
            await _repository.Save();
        }

        public async Task Update(ModalReservationDto reservationDto)
        {
            var existing = await _context.ReservationDetails.FirstOrDefaultAsync(x => x.Id == reservationDto.Id && !x.IsDeleted);
            if (existing == null) throw new Exception("Reservation not found");

            existing.ReservationDate = reservationDto.ReservationDate;
            existing.StartTime = reservationDto.StartTime;
            existing.EndTime = reservationDto.EndTime;
            existing.NumberOfGuest = reservationDto.NumberOfGuest;
            await _repository.Update(existing);
            await _repository.Save();
        }

        public async Task<object> GetPaginatedProducts(int pageNumber, int pageSize)
        {
            return await _customerRepository.GetPaginatedProducts(pageNumber, pageSize);
        }

        public async Task<object> GetReservation(string? customerId = null)
        {
            return await _customerRepository.GetAllReservation(customerId);
        }

        public async Task Delete(Guid id)
        {
            var data = await _repository.GetById(id);
            data.IsDeleted = true;
            data.ReservationStatus = ReservationStatus.Cancelled;
            await _repository.Update(data);
            await _repository.Save();
        }

        public async Task<ModalReservationDto> GetReservationById(Guid id)
        {
            var data = await _context.ReservationDetails
                .Include(x => x.Table)
                .FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<ModalReservationDto>(data);
        }
    }
}
