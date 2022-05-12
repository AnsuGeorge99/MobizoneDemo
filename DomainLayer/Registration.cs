using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DomainLayer
{
    public class Registration
    {
        [Key]
        public int registrationId { get; set; }
        [Required(ErrorMessage = "*First Name is required")]
        [Display(Name = "First Name")]
        public string firstName { get; set; }
        [Required(ErrorMessage = "*Last Name is required")]
        [Display(Name = "Last Name")]
        public string lastName { get; set; }
        [Required(ErrorMessage = "*Email Id is required")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "*Email address should be in the format adc@domain.com")]
        public string email { get; set; }
        [Required(ErrorMessage = "*Password is required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [RegularExpression("[^ ]{8,16}", ErrorMessage = "Password should contain a minimum of 8 characters and a capital letter")]
        public string password { get; set; }
        [Required(ErrorMessage = "*Password is required")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("password")]
        [NotMapped]
        public string confirmPassword { get; set; }
        public bool isActive { get; set; }
        [Column("CreatedOn")]
        public DateTime createdOn { get; set; }
        [Column("CreatedBy", TypeName = "nvarchar")]
        [MaxLength(150)]
        public string createdBy { get; set; }
        [Column("ModifiedOn")]
        public DateTime modifiedOn { get; set; }
        [Column("ModifiedBy", TypeName = "nvarchar")]
        [MaxLength(150)]
        public string modifiedBy { get; set; }
        public int roleId { get; set; }
        //public ICollection<Address>? address { get; set; }
    }
}

