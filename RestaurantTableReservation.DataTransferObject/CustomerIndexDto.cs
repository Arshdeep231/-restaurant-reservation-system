using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantTableReservation.DataTransferObject
{
    public class CustomerIndexDto : ModalReservationDto
    {
        public PagedResponse<TableDetailDto> TableData { get; set; }
        public PagedResponse<TableDetailDto> TableData1 { get; set; }
    }
}
