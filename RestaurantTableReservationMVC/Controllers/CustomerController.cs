using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestaurantTableReservation.DataTransferObject;
using System.Security.Claims;
using System.Text;

namespace RestaurantTableReservationMVC.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly HttpClient _httpClient;

        public CustomerController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 8)
        {
            var url = $"https://localhost:7017/api/customer/paginatedProducts?pageNumber={pageNumber}&pageSize={pageSize}";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                PagedResponse<TableDetailDto>? result = await response.Content.ReadFromJsonAsync<PagedResponse<TableDetailDto>>();
                var data = new CustomerIndexDto
                {
                    TableData = result
                };
                return View(data);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            var url = $"https://localhost:7017/api/Customer/getbyid?id={id}";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ModalTableDto>();
                return Json(result);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetResevationById(Guid id)
        {
            var url = $"https://localhost:7017/api/Customer/getresevationbyid?id={id}";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ModalReservationDto>();
                return Json(result);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ModalReservationDto reservationDto)
        {
            if (ModelState.IsValid)
            {
                var url = $"https://localhost:7017/api/Customer/create";
                reservationDto.CustomerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var json = JsonConvert.SerializeObject(reservationDto);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(url, stringContent);
                if (response.IsSuccessStatusCode)
                {
                    TempData["success"] = "Reservation confirmed.";
                    return RedirectToAction("MyReservations");
                }
            }
            TempData["error"] = "Reservation is not confirmed.";
            return RedirectToAction("index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(ModalReservationDto reservationDto)
        {
            var url = $"https://localhost:7017/api/Customer/update";
            reservationDto.CustomerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var json = JsonConvert.SerializeObject(reservationDto);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, stringContent);
            if (response.IsSuccessStatusCode)
            {
                TempData["success"] = "Reservation updated.";
                return RedirectToAction("MyReservations");
            }
            TempData["error"] = "Unable to update reservation. Slot may be unavailable.";
            return RedirectToAction("MyReservations");
        }

        [HttpGet]
        public async Task<IActionResult> MyReservations()
        {
            var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var url = $"https://localhost:7017/api/Customer/getallresevation?customerId={customerId}";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<ModalReservationDto>>();
                return View(result ?? new List<ModalReservationDto>());
            }
            return View(new List<ModalReservationDto>());
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var url = $"https://localhost:7017/api/Customer/Delete?id={id}";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                TempData["success"] = "Reservation cancelled.";
                return RedirectToAction("MyReservations");
            }
            return RedirectToAction("MyReservations");
        }
    }
}
