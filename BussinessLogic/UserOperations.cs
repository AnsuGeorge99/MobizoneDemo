using DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinesLogic.Interface;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using RepositoryLayer.Interface;
using RepositoryLayer;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace BusinesLogic
{
    public class UserOperations : IUserOperations
    {
        private readonly IGenericRepositoryOperation<Registration> _repositoryOperation;
        private readonly ProductDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public UserOperations(ProductDbContext dbContext, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _repositoryOperation = new GenericRepositoryOperation<Registration>(_dbContext);
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }
        public async Task<bool> Register(Registration register)
        //public async Task<bool> Register(Registration register)
        {
            /*try
            {
                var user = new ApplicationUser()
                {
                    firstName = register.firstName,
                    lastName = register.lastName,
                    Email = register.email,
                    UserName = register.email
                };
                var result = _userManager.CreateAsync(user, register.password);
                return await _userManager.AddToRoleAsync(user, UserRoles.User);
            }
            catch (Exception ex)
            {
                return null;
            }*/
            try
            {
                register.roleId = 1;
                _repositoryOperation.Add(register);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Registration> Authenticate(string username, string password)
        {
            Registration registration = new Registration();
            var list = _repositoryOperation.GetAll();
            var user = list.FirstOrDefault(users => users.email == username && users.password == password);

            return user;
        }

        public async Task Edit(Registration user)
        {
            _repositoryOperation.Update(user);
            _repositoryOperation.Save();
        }

        public async Task<string> Userlogin(Login login)
        {
            var user = await _userManager.FindByNameAsync(login.username);
            if (user != null && await _userManager.CheckPasswordAsync(user, login.password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return token.ToString();
            }
            return null;
           
        }

        public async Task<List<ApplicationUser>> GetUser()
        {
            //return _repositoryOperation.GetAll();
            var users = _userManager.Users.ToList();
            return users;
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
