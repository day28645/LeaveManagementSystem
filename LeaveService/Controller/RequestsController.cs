using LeaveService.DTO;
using LeaveService.Service;
using Microsoft.AspNetCore.Mvc;

namespace LeaveService.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class RequestsController : ControllerBase
    {
        private readonly IRequestService _service;

        public RequestsController(IRequestService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var request = await _service.GetAllRequestsAsync();
            return Ok(request);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRequestDto dto)
        {
            var request = await _service.CreateLRequestAsync(dto);
            return Ok(request);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateRequestDto dto)
        {
            var request = await _service.UpdateRequestAsync(id, dto);
            return Ok(request);
        }
    }
}