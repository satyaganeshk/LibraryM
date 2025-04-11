using LibraryM.DTO;
using LibraryM.Services.Interfaces;
using LibraryM.Entity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryM.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(BookCreateModel model)
        {
            return await _bookService.AddBookAsync(model);
        }


        [HttpGet]
        public async Task<IActionResult> GetBook(string bookId)
        {
            return await _bookService.GetBookAsync(bookId);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            return await _bookService.GetAllBooksAsync();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBook(BookModel model)
        {
            return await _bookService.UpdateBookAsync(model);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBook(string bookId)
        {
            return await _bookService.DeleteBookAsync(bookId);
        }
    }
}