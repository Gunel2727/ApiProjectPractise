using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class UiAccountController : Controller
    {
        private readonly HttpClient _httpClient;

        public UiAccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
      

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var json = JsonSerializer.Serialize(model);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(
                "http://localhost:5202/api/Account/register",
                content
            );

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Login");
            }

            // error olarsa
            ModelState.AddModelError("", "Registration failed");
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

    }
}
