using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using WebApplication1.Dtos;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("ApiClient");

            var response = await client.GetAsync(
                "http://localhost:5202/api/Product"
            );

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "UiAccount");
            }

            if (!response.IsSuccessStatusCode)
            {
                return View(new List<ProductReturnDto>());
            }

            var jsonData = await response.Content.ReadAsStringAsync();

            var products = JsonSerializer.Deserialize<List<ProductReturnDto>>(
                jsonData,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var client = _httpClientFactory.CreateClient("ApiClient");

           
            var categoryResponse = await client.GetAsync(
                "http://localhost:5202/api/Category"
            );

           
            var colorResponse = await client.GetAsync(
                "http://localhost:5202/api/Color"
            );

            var categoryJson = await categoryResponse.Content.ReadAsStringAsync();

            var colorJson = await colorResponse.Content.ReadAsStringAsync();

            var categories = JsonSerializer.Deserialize<List<CategoryReturnDto>>(
                categoryJson,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );

            var colors = JsonSerializer.Deserialize<List<ColorReturnDto>>(
                colorJson,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );

            ViewBag.Categories = categories;
            ViewBag.Colors = colors;

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateViewModel model)
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            if (!ModelState.IsValid)
            {
                var categoryResponse = await client.GetAsync("http://localhost:5202/api/Category");
                var colorResponse = await client.GetAsync("http://localhost:5202/api/Color");

                ViewBag.Categories = JsonSerializer.Deserialize<List<CategoryReturnDto>>(
                    await categoryResponse.Content.ReadAsStringAsync(), options) ?? new();
                ViewBag.Colors = JsonSerializer.Deserialize<List<ColorReturnDto>>(
                    await colorResponse.Content.ReadAsStringAsync(), options) ?? new();

                return View(model);
            }

            try
            {
                var productDto = new
                {
                    name = model.Name,
                    description = model.Description,
                    price = model.Price,
                    categoryId = model.CategoryId,
                    colorIds = model.ColorIds
                };

                var jsonContent = JsonSerializer.Serialize(productDto);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("http://localhost:5202/api/Product", httpContent);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    return RedirectToAction("Login", "UiAccount");

                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError(string.Empty, await response.Content.ReadAsStringAsync());

                    var categoryResponse = await client.GetAsync("http://localhost:5202/api/Category");
                    var colorResponse = await client.GetAsync("http://localhost:5202/api/Color");

                    ViewBag.Categories = JsonSerializer.Deserialize<List<CategoryReturnDto>>(
                        await categoryResponse.Content.ReadAsStringAsync(), options) ?? new();
                    ViewBag.Colors = JsonSerializer.Deserialize<List<ColorReturnDto>>(
                        await colorResponse.Content.ReadAsStringAsync(), options) ?? new();

                    return View(model);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError(string.Empty, $"API connection error: {ex.Message}");

                var categoryResponse = await client.GetAsync("http://localhost:5202/api/Category");
                var colorResponse = await client.GetAsync("http://localhost:5202/api/Color");

                ViewBag.Categories = JsonSerializer.Deserialize<List<CategoryReturnDto>>(
                    await categoryResponse.Content.ReadAsStringAsync(), options) ?? new();
                ViewBag.Colors = JsonSerializer.Deserialize<List<ColorReturnDto>>(
                    await colorResponse.Content.ReadAsStringAsync(), options) ?? new();

                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unexpected error: {ex.Message}");

                var categoryResponse = await client.GetAsync("http://localhost:5202/api/Category");
                var colorResponse = await client.GetAsync("http://localhost:5202/api/Color");

                ViewBag.Categories = JsonSerializer.Deserialize<List<CategoryReturnDto>>(
                    await categoryResponse.Content.ReadAsStringAsync(), options) ?? new();
                ViewBag.Colors = JsonSerializer.Deserialize<List<ColorReturnDto>>(
                    await colorResponse.Content.ReadAsStringAsync(), options) ?? new();

                return View(model);
            }
        }
    }
}
