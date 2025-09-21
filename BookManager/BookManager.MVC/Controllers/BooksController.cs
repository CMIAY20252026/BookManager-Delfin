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
            _httpClient = httpClientFactory.CreateClient("BookApi");
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            if (!ModelState.IsValid)
                return View(book);

            var response = await _httpClient.PostAsJsonAsync("books", book);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error saving book.");
            return View(book);
        }

     
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _httpClient.GetFromJsonAsync<Book>($"books/{id}");
            if (book == null)
                return NotFound();

            return View(book);
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Book book)
        {
            if (id != book.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(book);

            var response = await _httpClient.PutAsJsonAsync($"books/{id}", book);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error updating book.");
            return View(book);
        }

    
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _httpClient.GetFromJsonAsync<Book>($"books/{id}");
            if (book == null)
                return NotFound();

            return View(book);
        }

     
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"books/{id}");

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error deleting book.");
            return RedirectToAction(nameof(Index));
        }
    }
}
