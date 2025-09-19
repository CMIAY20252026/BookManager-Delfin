using Microsoft.AspNetCore.Mvc;
using BookManager.WebAPI.Models;
using System.Collections.Generic;

namespace BookManager.WebAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class BooksController : ControllerBase
	{
		private static List<Book> books = new List<Book>();

		[HttpGet]
		public IActionResult GetBooks()
		{
			return Ok(books);
		}

		[HttpPost]
		public IActionResult AddBook([FromBody] Book book)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			books.Add(book);
			return Ok(books);
		}
	}
}
