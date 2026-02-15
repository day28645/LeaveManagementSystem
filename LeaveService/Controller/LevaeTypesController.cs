using LeaveService.DTO;
using LeaveService.Service;
using Microsoft.AspNetCore.Mvc;

namespace LeaveService.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class LevaeTypesController : ControllerBase
    {
        private readonly ILeaveTypeService _service;

        public LevaeTypesController(ILeaveTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var type = await _service.GetAllLevaeTypesAsync();
            return Ok(type);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateLeaveTypeDto dto)
        {
            var type = await _service.CreateLeaveTypeAsync(dto);
            return Ok(type);
        }
    }
}