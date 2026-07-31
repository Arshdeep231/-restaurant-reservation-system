using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestaurantTableReservation.DataTransferObject;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace RestaurantTableReservationMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;

        public AccountController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                var role = User.FindFirstValue(ClaimTypes.Role);
                if (role == "admin")
                {
                    return RedirectToAction("index", "admin");
                }
                return RedirectToAction("index", "customer");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto login)
        {
            if (ModelState.IsValid)
            {
                var url = "https://localhost:7017/api/Account/login";
                var json = JsonConvert.SerializeObject(login);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(url, stringContent);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<AccountResponseDto>();
                    if (result.Status)
                    {
                        if (result.Data != null)
                        {
                            Response.Cookies.Append("JwtToken", result.Data, new CookieOptions
                            {
                                HttpOnly = true,
                                Secure = true,
                                SameSite = SameSiteMode.Strict,
                                Expires = DateTime.UtcNow.AddDays(7)
                            });
                            var token = result.Data;
                            var handler = new JwtSecurityTokenHandler();
                            var jwtToken = handler.ReadJwtToken(token);
                            var claimIdentity = new ClaimsIdentity(jwtToken.Claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var principle = new ClaimsPrincipal(claimIdentity);
                            var authProperties = new AuthenticationProperties
                            {
                                IsPersistent = true,
                                ExpiresUtc = DateTime.UtcNow.AddDays(7)
                            };
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principle, authProperties);
                            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Data);
                            TempData["success"] = result.Message;
                            var role = principle.FindFirstValue(ClaimTypes.Role);
                            if (role == "admin")
                            {
                                return RedirectToAction("index", "admin");
                            }
                            return RedirectToAction("index", "Customer");
                        }
                    }
                    TempData["error"] = result.Message;
                    return RedirectToAction("login");
                }
            }
            return View(login);
        }

        public async Task<IActionResult> Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                var role = User.FindFirstValue(ClaimTypes.Role);
                if (role == "admin")
                {
                    return RedirectToAction("index", "home");
                }
                return RedirectToAction("index", "home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto register)
        {
            if (ModelState.IsValid)
            {
                var url = "https://localhost:7017/api/Account/register";
                var json = JsonConvert.SerializeObject(register);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(url, stringContent);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<AccountResponseDto>();
                    if (result.Status)
                    {
                        TempData["success"] = result.Message;
                        return RedirectToAction("login");
                    }
                    TempData["error"] = result.Message;
                    return RedirectToAction("register");
                }
            }
            return View(register);
        }


        public async Task<IActionResult> Logout()
        {
            var url = "https://localhost:7017/api/Account/logout";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<AccountResponseDto>();
                if (result.Status)
                {
                    foreach(var cookies in Request.Cookies.Keys)
                    {
                         Response.Cookies.Delete(cookies);
                    }
                    TempData["success"] = result.Message;
                    return RedirectToAction("login");
                }
                TempData["error"] = result.Message;
                return View();
            }
            return View();
        }


    }
}
