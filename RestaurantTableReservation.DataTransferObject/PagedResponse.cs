using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantTableReservation.DataTransferObject
{
    public class PagedResponse<T> where T : class
    {
        public int TotalRecord { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public List<T> data{ get; set; }
    }
}
