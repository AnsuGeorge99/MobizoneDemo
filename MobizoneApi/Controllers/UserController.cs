using APILayer.Models;
using BusinesLogic.Interface;
using DomainLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserOperations _userOperations;
        private readonly ILogger<UserController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UserController(IUserOperations userOperations, ILogger<UserController> logger, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _userOperations = userOperations;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] Registration register)
        {
            try 
            {
                var result = await _userOperations.Register(register);
                if (result)
                {
                    return Ok(new UserResponse<string> { status = "Success", message = "User registration is successfully completed"}) ;
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new UserResponse<string> { status = "Error", message = "User registration failed, please try again" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("error");
                _logger.LogError("error");
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] Login login)
        {
            try
            {
                Login loginData = new Login();
                loginData.username = login.username;
                loginData.password = login.password;
                var result = await _userOperations.Authenticate(loginData.username, loginData.password);
                if (result != null && result.roleId==1)
                {
                    return Ok(new UserResponse<string> { status = "Success", message = "Login Successfull" });
                    
                }
                else
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, new UserResponse<string> { status = "Error", message = "Login failed, please try again. If not registered, please Register to order your product" });
                }
            }
            catch (Exception ex) 
            {
                _logger.LogInformation("error");
                _logger.LogError("error");
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
        }
        [HttpGet("GetUser")]
        public  IEnumerable<ApplicationUser> GetUser()
        {
            return _userOperations.GetUser().Result;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgetPassword()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error to reset password", ex);
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }
        [HttpPost("AdminLogin")]
        public async Task<IActionResult> AdminLogin([FromBody] Login login)
        {
            try
            {
                Login loginData = new Login();
                loginData.username = login.username;
                loginData.password = login.password;
                var result = await _userOperations.Authenticate(loginData.username, loginData.password);
                if (result != null && result.roleId == 2)
                {
                    return Ok(new UserResponse<string> { status = "Success", message = "Login Successfull" });

                }
                else
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, new UserResponse<string> { status = "Error", message = "Login failed, please try again. If not registered, please Register to order your product" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("error");
                _logger.LogError("error");
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
        }
    }
}
