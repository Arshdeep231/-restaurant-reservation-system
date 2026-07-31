using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantTableReservation.BusinessAccessLayer;
using RestaurantTableReservation.DataTransferObject;

namespace RestaurantTableReservationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("changestatus")]
        public async Task<IActionResult> ChangeStatus([FromQuery] string userId, [FromQuery] bool userStatus)
        {
            return Ok(await _adminService.ChangeStatus(userId, userStatus));
        }

        [HttpGet("allpendingCustumer")]
        public async Task<IActionResult> PendingCustumer()
        {
            return Ok(await _adminService.AllCustomers());
        }

        [HttpGet("allreservations")]
        public async Task<IActionResult> AllReservations()
        {
            return Ok(await _adminService.GetAllReservations());
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(ModalTableDto tableDto)
        {
            try
            {
                await _adminService.CreateTable(tableDto);
                return Ok("Done");
            }
            catch (Exception ex) 
            {
             return BadRequest(ex.Message);
            }
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(ModalTableDto tableDto)
        {
            try
            {
                await _adminService.UpdateTable(tableDto);
                return Ok("Done");
            }
            catch (Exception ex) 
            {
             return BadRequest(ex.Message);
            }
        }

        [HttpGet("delete")]
        public async Task<IActionResult> Delete([FromQuery]Guid id)
        {
            try
            {
                await _adminService.DeleteTable(id);
                return Ok("Done");
            }
            catch (Exception ex) 
            {
             return BadRequest(ex.Message);
            }
        }

        [HttpGet("getalltable")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
               
                return Ok(await _adminService.GetAllTableDetails());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById([FromQuery]Guid id)
        {
            try
            {

                return Ok(await _adminService.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
