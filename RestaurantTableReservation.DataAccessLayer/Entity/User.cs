using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantTableReservation.DataAccessLayer.Entity
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public UserStatus Status { get; set; }
        public DateTime CreatedDate{ get; set; }
    }
}
