using LibraryM.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using LibraryM.Entity;

namespace LibraryM.Services.Interfaces
{
    public interface IBookService
    {
        Task<IActionResult> AddBook(BookCreateModel model);

        Task<IActionResult> GetBookAsync(string bookId);
        Task<IActionResult> GetAllBooksAsync();
        Task<IActionResult> UpdateBookAsync(BookModel model);
        Task<IActionResult> DeleteBookAsync(string bookId);
    }
}