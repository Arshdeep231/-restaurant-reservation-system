using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantTableReservation.BusinessAccessLayer;
using RestaurantTableReservation.DataTransferObject;
using System.Security.Claims;

namespace RestaurantTableReservationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _services;
        private readonly IAdminService _adminService;

        public CustomerController(ICustomerService service, IAdminService adminService)
        {
            _services = service;
            _adminService = adminService;
        }

        [HttpGet]
        [Route("paginatedProducts")]
        public async Task<IActionResult> GetPaginatedProducts([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            return Ok(await _services.GetPaginatedProducts(pageNumber, pageSize));
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById([FromQuery]Guid id)
        {
            return Ok(await _adminService.GetById(id));
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(ModalReservationDto reservationDto)
        {
            try
            {
                await _services.Create(reservationDto);
            return Ok("done");
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("update")]
        public async Task<IActionResult> Update(ModalReservationDto reservationDto)
        {
            try
            {
               await _services.Update(reservationDto);
               return Ok("done");
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("delete")]
        public async Task<IActionResult> Delete([FromQuery]Guid id)
        {
            try
            {

            await _services.Delete(id);
            return Ok("true");
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getallresevation")]
        public async Task<IActionResult> GetAllResevation([FromQuery] string? customerId = null)
        {
            try
            {
                return Ok(await _services.GetReservation(customerId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getresevationbyid")]
        public async Task<IActionResult> GetResevationById([FromQuery]Guid id)
        {
            try
            {
                return Ok(await _services.GetReservationById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
