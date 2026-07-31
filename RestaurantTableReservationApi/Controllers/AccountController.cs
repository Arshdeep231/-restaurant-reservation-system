using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantTableReservation.BusinessAccessLayer;
using RestaurantTableReservation.DataTransferObject;

namespace RestaurantTableReservationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto login)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _accountService.Login(login));
            }
            return BadRequest("Model is not valid");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto register)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _accountService.Register(register));
            }
            return BadRequest("Model is not valid");
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            return Ok(await _accountService.Logout());
        }

      
    }
}
