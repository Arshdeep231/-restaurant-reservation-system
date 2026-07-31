using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantTableReservation.DataTransferObject
{
    public class AdminDto : ModalTableDto
    {
        public List<TableDetailDto> TableDto { get; set; }
    }
}
