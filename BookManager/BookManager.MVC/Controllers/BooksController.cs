using Microsoft.AspNetCore.Mvc;
using BookManager.MVC.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BookManager.MVC.Controllers
{
    public class BooksController : Controller
    {
        private readonly HttpClient _httpClient;

        public BooksController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new System.Uri("https://localhost:7280/api/");
        }

        public async Task<IActionResult> Index()
        {
            var books = await _httpClient.GetFromJsonAsync<List<Book>>("books");
            return View(books ?? new List<Book>());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            if (!ModelState.IsValid)
                return View(book);

            var response = await _httpClient.PostAsJsonAsync("books", book);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Error saving book.");
            return View(book);
        }
    }
}
