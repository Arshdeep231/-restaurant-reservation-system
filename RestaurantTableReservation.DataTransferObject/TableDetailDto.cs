using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantTableReservation.DataTransferObject
{
    public class TableDetailDto
    {
        public Guid Id { get; set; }
        public int TableNumber { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
