using DomainLayer;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogic.Interface
{
    public interface IUserOperations
    {
        Task<bool> Register(Registration register);
        //Task<bool> Register(Registration register);
        Task<Registration> Authenticate(string username, string password);
        Task Edit(Registration user);
        Task<string> Userlogin(Login login);
        Task<List<ApplicationUser>> GetUser();
    }
}
