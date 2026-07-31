using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantTableReservation.DataAccessLayer.Entity
{
    public class ReservationDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid TableId { get; set; }
        public string CustomerId { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReservationDate { get; set; } = DateTime.Now;
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public int NumberOfGuest { get; set; }
        public ReservationStatus ReservationStatus { get; set; } = ReservationStatus.Booking;
        [DataType(DataType.Time)]
        public DateTime StartTime { get; set; }
        [DataType(DataType.Time)]
        public DateTime EndTime { get; set; }
        public bool IsDeleted { get; set; } = false;
        [ForeignKey(nameof(ReservationDetail.TableId))]
        public TableDetail Table { get; set; }  

    }
}
