using RestaurantTableReservation.DataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantTableReservation.DataTransferObject
{
    public class ModalReservationDto
    {
        public Guid Id { get; set; }
        public Guid TableId { get; set; }
        public string? CustomerId { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReservationDate { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public int NumberOfGuest { get; set; }

        [DataType(DataType.Time)]
        public DateTime StartTime { get; set; }
        [DataType(DataType.Time)]
        public DateTime EndTime { get; set; }
        public bool IsDeleted { get; set; } = false;

        public TableDetail? Table { get; set; }
    }
}
