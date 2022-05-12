using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APILayer.Models
{
    public class UserResponse<T>
    {
        public string status { get; set; }
        public string message { get; set; }
        public string token { get; set; }
        public T result { get; set; }
    }

    
}
