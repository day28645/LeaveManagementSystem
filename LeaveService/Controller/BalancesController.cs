using LeaveService.DTO;
using LeaveService.Service;
using Microsoft.AspNetCore.Mvc;

namespace LeaveService.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class BalancesController : ControllerBase
    {
        private readonly IBalanceService _service;

        public BalancesController(IBalanceService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateBalanceDto dto)
        {
            var balance = await _service.CreateBalanceAsync(dto);
            return Ok(balance);
        }

        [HttpGet("by-id/{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var balance = await _service.GetLeaveBalanceByIdAsync(id);

            if(!balance.IsSuccess)
            {
                return NotFound(balance.Message);
            }

            return Ok(balance);
        }

        [HttpGet("by-user/{userid}")]
        public async Task<IActionResult> GetByUserId(Guid userid)
        {
            var balance = await _service.GetBalanceByUserIdAsync(userid);

            if(!balance.IsSuccess)
            {
                return NotFound(balance.Message);
            }

            return Ok(balance);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateBalanceDto dto)
        {
            var balance = await _service.UpdateBalanceAsync(id, dto);

            if(balance == null)
            {
                return NotFound();
            }

            return Ok(balance);
        }
    }
}