using LibraryM.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LibraryM.Services.Interfaces
{
    public interface ILibrarianService
    {
        Task<IActionResult> SignUp(LibrarianModel model);
        Task<IActionResult> Login(string emailId, string password);
        Task<IActionResult> GetLibrarian(string uid);
        Task<IActionResult> GetAllLibrarians();
        Task<IActionResult> UpdateLibrarian(LibrarianUpdateModel model);
        Task<IActionResult> DeleteLibrarian(string uid);

        Task<IActionResult> ShowIssuedBooksAsync();
    }
}
