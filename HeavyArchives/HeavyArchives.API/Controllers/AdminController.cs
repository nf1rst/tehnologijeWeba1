using EncyclopaediaBisemutum.Application.DTOs.Auth;
using EncyclopaediaBisemutum.Application.DTOs.User;
using EncyclopaediaBisemutum.Application.Interfaces;
using Exceptions;
using FluentValidation;
using HeavyArchives.Application.DTOs.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace EncyclopaediaBisemutum.API.Controllers
{
    //https://learn.microsoft.com/en-us/aspnet/core/security/authorization/simple?view=aspnetcore-10.0
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles="Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService adminService;
        private readonly ILogger<AdminController> logger;

        public AdminController(IAdminService adminService, ILogger<AdminController> logger)
        {
            this.adminService = adminService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            ICollection<UserDTO> users = await adminService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id:int}")] // inace ih registrira kao istu rutu jer je samo vrijabla 
        public async Task<IActionResult> GetUserById(int id)
        {
            UserWithDetailsDTO? user = await adminService.GetUserByIdAsync(id);
            return Ok(user);
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetUserByUsername([FromRoute] string username)
        {
            if (username is null) throw new InvalidInputException("Username cannot be null");

            UserWithDetailsDTO? user = await adminService.GetUserByUsernameAsync(username);
            return Ok(user);
        }

        [HttpPost]
        //https://github.com/serilog/serilog
        //@ destrukturira/serijalizira {} toString
        public async Task<IActionResult> CreateAdminUser(UserRegisterWithDetailsDTO login)
        {   
           UserWithDetailsDTO? user= await adminService.AddAdminUser(login);
            logger.LogInformation("[{Controller}] Admin user created: {@User}", nameof(AdminController), user);
            return CreatedAtAction(nameof(GetUserById), new {id=user.Id}, user);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await adminService.DeleteUserAsync(id);
            return NoContent();
        }

        [HttpPatch("promote/{username}")]
        public async Task<IActionResult> PromoteUser(string username)
        {
            var user = await adminService.PromoteUser(username);
            return Ok(user);
        }
        
    }
}
