using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UnityApp.Models;

namespace UnityApp.Controllers
{
    public class AuthorController : Controller


    {
        private readonly ApiService _apiService;

        public AuthorController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var authors = await _apiService.GetAllAuthors();
                return View("Index", authors);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return View("Error");
            }
        }


        // DELETE: api/authors/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                // Call the DeleteAuthor method from ApiService
                await _apiService.DeleteAuthor(id);

                return NoContent(); // Return 204 No Content on successful deletion
            }
            catch (Exception ex)
            {
                // Handle the exception and return an appropriate error response
                return BadRequest($"Failed to delete author. Error: {ex.Message}");
            }
        }
 

    // GET: AuthorController
    /* public ActionResult Index()
     {
         return View();
     }*/

    // GET: AuthorController/Details/5
    public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create(AuthorDTO authorDto)
        {
            if (ModelState.IsValid)
            {
                await _apiService.AddAuthor(authorDto);
                return RedirectToAction(nameof(Index));
            }

            return View(authorDto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var author = await _apiService.GetAuthorForEdit(id);
            if (author == null)
            {
                return NotFound();
            }

            return View("Edit",author);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AuthorDTO authorDto)
        {
            if (ModelState.IsValid)
            {
                await _apiService.UpdateAuthor(id, authorDto);
                return RedirectToAction(nameof(Index));
            }

            // If the model is not valid, return to the edit view with the current data
            return View(authorDto);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
          /*  var author = await _apiService.GetAuthorById(id);
            if (author == null)
            {
                return NotFound();
            }*/

            // Call your API to delete the author and related posts
            await _apiService.DeleteAuthor(id);

            // Redirect back to the list of authors
            return RedirectToAction(nameof(Index));
        }
    }
}
