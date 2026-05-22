using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using WebApplication1.Dtos;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public async Task<IActionResult> Index()
        {
            try
            {
                var _httpClient = _httpClientFactory.CreateClient("ApiClient");

                var response = await _httpClient.GetAsync(
                    "http://localhost:5202/api/Category"
                );

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "UiAccount");
                }


                if (!response.IsSuccessStatusCode)
                {
                    TempData["ErrorMessage"] = $"Failed to retrieve categories. Status: {response.StatusCode}";
                    return View(new List<CategoryReturnDto>());
                }

                var jsonData = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(jsonData))
                {
                    TempData["ErrorMessage"] = "No data received from API.";
                    return View(new List<CategoryReturnDto>());
                }

                var categories = JsonSerializer.Deserialize<List<CategoryReturnDto>>(
                    jsonData,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                );

                if (categories == null)
                {
                    TempData["ErrorMessage"] = "Failed to deserialize categories.";
                    return View(new List<CategoryReturnDto>());
                }

                return View(categories);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                return View(new List<CategoryReturnDto>());
            }
        }


        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                var _httpClient = _httpClientFactory.CreateClient("ApiClient");

                var response = await _httpClient.GetAsync(
                    $"http://localhost:5202/api/Category/{id}"
                );
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "UiAccount");
                }



                if (!response.IsSuccessStatusCode)
                {
                    TempData["ErrorMessage"] = $"Failed to retrieve category details. Status: {response.StatusCode}";
                    return NotFound();
                }

                var jsonData = await response.Content.ReadAsStringAsync();

                var category = JsonSerializer.Deserialize<CategoryReturnDto>(
                    jsonData,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                );

                if (category == null)
                {
                    return NotFound();
                }

                return View(category);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                return NotFound();
            }
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var client = _httpClientFactory.CreateClient("ApiClient");

            var content = new MultipartFormDataContent();

            content.Add(
                new StringContent(model.Name),
                "Name"
            );

            content.Add(
                new StringContent(model.Description),
                "Description"
            );

            if (model.Photo != null)
            {
                var fileContent = new StreamContent(model.Photo.OpenReadStream());

                fileContent.Headers.ContentType =
                    new MediaTypeHeaderValue(model.Photo.ContentType);

                content.Add(
                    fileContent,
                    "Photo",
                    model.Photo.FileName
                );
            }

            var response = await client.PostAsync(
                "http://localhost:5202/api/Category",
                content
            );

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "UiAccount");
            }

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();

                ModelState.AddModelError("", error);

                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }




    }
}
