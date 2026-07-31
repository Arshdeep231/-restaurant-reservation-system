using System;

namespace RestaurantTableReservation.DataTransferObject
{
    public class ReservationOverviewDto
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; } = "—";
        public int TableNumber { get; set; }
        public int NumberOfGuest { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; } = "Booked";
        public DateTime CreateAt { get; set; }
    }
}
