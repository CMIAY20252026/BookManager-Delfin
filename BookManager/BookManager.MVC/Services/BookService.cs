using System.Net.Http;
using System.Net.Http.Json;
using BookManager.MVC.Models;

namespace BookManager.MVC.Services
{
    public class BookService
    {
        private readonly HttpClient _httpClient;

        public BookService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7031/api/");
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            var books = await _httpClient.GetFromJsonAsync<IEnumerable<Book>>("books");
            return books ?? Enumerable.Empty<Book>();
        }

        public async Task<Book?> GetByIdAsync(int id) =>
            await _httpClient.GetFromJsonAsync<Book>($"books/{id}");

        public async Task CreateAsync(Book book) =>
            await _httpClient.PostAsJsonAsync("books", book);

        public async Task UpdateAsync(int id, Book book) =>
            await _httpClient.PutAsJsonAsync($"books/{id}", book);

        public async Task DeleteAsync(int id) =>
            await _httpClient.DeleteAsync($"books/{id}");
    }
}
