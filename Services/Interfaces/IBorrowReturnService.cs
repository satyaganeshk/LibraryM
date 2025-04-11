using System.Collections.Generic;
using LibraryM.Entity;
using Microsoft.AspNetCore.Mvc;



namespace LibraryM.Services.Interfaces
{
    public interface IBorrowReturnService
    {
        Task<IActionResult> IssueBookAsync(int studentPrnNumber, string bookId);
        Task<IActionResult> ReturnBookAsync(int studentPrnNumber, string bookId);
    }
}
