using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantTableReservation.DataTransferObject
{
    public class RegisterDto
    {
        [Required]
        [RegularExpression("^[A-Za-z]+(?:[ '-][A-Za-z]+)*$" ,ErrorMessage ="Enter a valid Name")]
        public string Name { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$",ErrorMessage ="enter a vaild email")]
        public string  Email { get; set; }
        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$",ErrorMessage ="Enter a strong Password atlest 8 charcter one upper one lower and special character")]
        public string  Password { get; set; }
        [Compare("Password",ErrorMessage ="not match")]
        public string ConfirmPassword { get; set; }
    }
}
