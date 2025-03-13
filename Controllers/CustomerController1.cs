using Microsoft.AspNetCore.Mvc;
using WebMVC_CallApi.Models;

namespace WebMVC_CallApi.Controllers
{
    public class CustomerController : Controller
    {
        private readonly HttpClient _httpClient;
        private const string ApiBaseUrl = "http://localhost:5066/api/customers";

        public CustomerController()
        {
            int u = 7;
            _httpClient = new HttpClient();
        }

        public async Task<IActionResult> Index()
        {
            var customers = await _httpClient.GetFromJsonAsync<List<Customer>>(ApiBaseUrl);
            return View(customers);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            var response = await _httpClient.PostAsJsonAsync(ApiBaseUrl, customer);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View(customer);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _httpClient.GetFromJsonAsync<Customer>($"{ApiBaseUrl}/{id}");
            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Customer customer)
        {
            var response = await _httpClient.PutAsJsonAsync($"{ApiBaseUrl}/{customer.Id}", customer);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View(customer);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"{ApiBaseUrl}/{id}");
            return RedirectToAction("Index");
        }
    }
}
