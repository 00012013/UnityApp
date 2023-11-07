using Microsoft.AspNetCore.Mvc;
using UnityApp.Models;

namespace UnityApp.Controllers
{
    public class BookController : Controller
    {
        private readonly ApiService _apiService;

        public BookController(ApiService apiService)
        {
            _apiService = apiService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var books = await _apiService.GetAllBooks();
            return View("BookList",books);
        }
        public IActionResult Create()
        {
            var authors = _apiService.GetAllAuthors().Result;

            ViewBag.Authors = authors;
            return View("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookDTO book)
        {
            if (ModelState.IsValid)
            {
                // Call your API to add the book
                // You need to implement AddBook method in your ApiService
                await _apiService.AddBook(book);

                return RedirectToAction(nameof(Index));
            }

            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBook(int id)
        {
            // Call your API to delete the book
            await _apiService.DeleteBookById(id);

            // Redirect back to the list of books
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            // Fetch the book details from the API
            var book = await _apiService.GetBookById(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> EditBook(BookDTO book)
        {
            await _apiService.UpdateBook(book.Id, book);

            return RedirectToAction(nameof(Index));
        }
    }
}
