using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    class Address
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string district { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string pincode { get; set; }
        public string phoneNumber { get; set; }
        public string additionalInfo { get; set; }
        [NotMapped]
        public bool IsChecked { get; set; }
    }
}
