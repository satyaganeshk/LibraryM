using LibraryM.DTO;
using LibraryM.Services.Interfaces;
using LibraryM.Entity;
using Microsoft.AspNetCore.Mvc;
namespace LibraryM.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BorrowReturnController: ControllerBase
    {
        private readonly IBorrowReturnService _borrowReturnService;

        public BorrowReturnController(IBorrowReturnService borrowReturnService)
        {
            _borrowReturnService = borrowReturnService;
        }
        [HttpGet]
        public async Task<IActionResult> IssueBookAsync(int studentPrnNumber, string bookId)
        {
            return await _borrowReturnService.IssueBookAsync(studentPrnNumber, bookId);
        }

        [HttpGet]
        public async Task<IActionResult> ReturnBookAsync(int studentPrnNumber, string bookId)
        {
            return await _borrowReturnService.ReturnBookAsync(studentPrnNumber, bookId);
        }
    }
}
