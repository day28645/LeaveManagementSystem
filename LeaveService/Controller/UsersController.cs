using LeaveService.DTO;
using LeaveService.Service;
using Microsoft.AspNetCore.Mvc;

namespace LeaveService.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _service.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDto dto)
        {
            var user = await _service.CreateUserAsync(dto);
            return Ok(user);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetById(Guid userId)
        {
            var balance = await _service.GetUserReportToByIdAsync(userId);

            if(!balance.IsSuccess)
            {
                return NotFound(balance.Message);
            }

            return Ok(balance);
        }

    }
}