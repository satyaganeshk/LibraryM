using LibraryM.DTO;
using LibraryM.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LibraryM.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LibrarianController : ControllerBase
    {
        private readonly ILibrarianService _librarianService;

        public LibrarianController(ILibrarianService librarianService)
        {
            _librarianService = librarianService;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] LibrarianModel model)
        {
            return await _librarianService.SignUp(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(string emailId, string password)
        {
            return await _librarianService.Login(emailId, password);
        }

        [HttpGet]
        public async Task<IActionResult> GetLibrarian(string uid)
        {
            return await _librarianService.GetLibrarian(uid);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLibrarians()
        {
            return await _librarianService.GetAllLibrarians();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] LibrarianUpdateModel model)
        {
            return await _librarianService.UpdateLibrarian(model);
        }

        [HttpGet]
        public async Task<IActionResult> ShowIssuedBooks()
        {
            return await _librarianService.ShowIssuedBooksAsync();
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(string uid)
        {
            return await _librarianService.DeleteLibrarian(uid);
        }
    }
}
