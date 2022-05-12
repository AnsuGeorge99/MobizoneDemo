using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public class Login
    {
        [Key]
        public int loginId { get; set; }
        [Required(ErrorMessage = "*Username is required")]
        [Display(Name = "Username")]
        [EmailAddress(ErrorMessage = "*Username should be in the format adc@domain.com")]
        public string username { get; set; }
        [Required(ErrorMessage = "*Password is required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [RegularExpression("[^ ]{8,16}", ErrorMessage = "Password should contain a minimum of 8 characters and a capital letter")]
        public string password { get; set; }
        public int roleId { get; set; }
        public DateTime createdOn { get; set; }
        public string createdBy { get; set; }
        public DateTime modifiedOn { get; set; }
        public string modifiedBy { get; set; }
    }
}
