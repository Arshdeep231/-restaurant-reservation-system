using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestaurantTableReservation.DataAccessLayer.Entity;
using RestaurantTableReservation.DataTransferObject;
using System.Text;

namespace RestaurantTableReservationMVC.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly HttpClient _httpClient;

        public AdminController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var url = "https://localhost:7017/api/Admin/getalltable";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<TableDetailDto>>();
                var data = new AdminDto
                {
                    TableDto = result
                };
                return View(data);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ModalTableDto tableDto)
        {
            var url = "https://localhost:7017/api/Admin/create";
            var json = JsonConvert.SerializeObject(tableDto);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, stringContent);
            if (response.IsSuccessStatusCode)
            {
                TempData["success"] = "Table is created";
                return RedirectToAction("index");
            }
            TempData["error"] = "Table is not created";
            return RedirectToAction("index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(ModalTableDto tableDto)
        {
            var url = "https://localhost:7017/api/Admin/update";
            var json = JsonConvert.SerializeObject(tableDto);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, stringContent);
            if (response.IsSuccessStatusCode)
            {
                TempData["success"] = "Table is Updated";
                return RedirectToAction("index");
            }
            TempData["error"] = "Table is not Update";
            return RedirectToAction("index");
        }

        public async Task<IActionResult> GetById(Guid id)
        {
            var url = $"https://localhost:7017/api/Admin/getbyid?id={id}";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ModalTableDto>();
                return Json(result);
            }
            return RedirectToAction("index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var url = $"https://localhost:7017/api/Admin/delete?id={id}";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                TempData["success"] = "Table Deleted Successfully";
                return RedirectToAction("index");
            }
            TempData["error"] = "Unable to delete table. It may have future reservations.";
            return RedirectToAction("index");
        }

        public async Task<IActionResult> PendingUser()
        {
            var url = "https://localhost:7017/api/Admin/allpendingCustumer";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<IList<User>>();
                return View(data);
            }
            return View();
        }

        public async Task<IActionResult> Reservations()
        {
            var url = "https://localhost:7017/api/Admin/allreservations";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<List<ReservationOverviewDto>>();
                return View(data ?? new List<ReservationOverviewDto>());
            }
            return View(new List<ReservationOverviewDto>());
        }

        public async Task<IActionResult> ChangeStatus(string userId, bool userStatus)
        {
            var url = $"https://localhost:7017/api/Admin/changestatus?userId={userId}&userStatus={userStatus}";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<AccountResponseDto>();
                if (result.Status)
                {
                    TempData["success"] = result.Message;
                    return RedirectToAction("PendingUser");
                }
                TempData["error"] = result.Message;
                return RedirectToAction("PendingUser");
            }
            return RedirectToAction("PendingUser");
        }
    }
}
