// IBorrowReturnService.cs
using LibraryM.DTO;
using LibraryM.Entity;
using LibraryM.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryM.Services
{
    public class BorrowReturnService : IBorrowReturnService
    {
        private readonly Container _container;

        public BorrowReturnService(CosmosClient client)
        {
            _container = client.GetDatabase("library").GetContainer("library");
        }

        public async Task<IActionResult> IssueBookAsync(int studentPrnNumber, string bookId)
        {
            var student = _container.GetItemLinqQueryable<Student>(true)
                .Where(s => s.PrnNumber == studentPrnNumber && s.Active && !s.Archieved)
                .AsEnumerable().FirstOrDefault();

            var book = _container.GetItemLinqQueryable<Book>(true)
                .Where(b => b.BookId == bookId && b.Active && !b.Archieved)
                .AsEnumerable().FirstOrDefault();

            if (student == null || book == null)
            {
                return new NotFoundObjectResult("Student or Book not found.");
            }
            string newId = Guid.NewGuid().ToString();
            var borrow = new BorrowReturn
            {
                Id = newId,
                UId = newId,
                BookUid = bookId,
                bookIssue = true,
                returnBook = false,
                IssueDate = DateTime.UtcNow,
                ReturnDate = DateTime.UtcNow.AddDays(7),
                DocumentType = "BorrowReturn",
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                Version = 1,
                Active = true,
                Archieved = false
            };

            await _container.CreateItemAsync(borrow);
            return new OkObjectResult("Book issued successfully.");
        }

        public async Task<IActionResult> ReturnBookAsync(int studentPrnNumber, string bookId)
        {
            var borrow = _container.GetItemLinqQueryable<BorrowReturn>(true)
                .Where(b => b.BookUid == bookId && b.bookIssue && !b.returnBook && b.Active && !b.Archieved)
                .AsEnumerable().FirstOrDefault();

            var student = _container.GetItemLinqQueryable<Student>(true)
                .Where(s => s.PrnNumber == studentPrnNumber && s.Active && !s.Archieved)
                .AsEnumerable().FirstOrDefault();

            if (borrow == null || student == null)
            {
                return new NotFoundObjectResult("No borrowed book found for this student.");
            }
            string newId = Guid.NewGuid().ToString();
            var returnEntry = new BorrowReturn
            {
                Id = newId,
                UId = newId,
                BookUid = bookId,
                bookIssue = false,
                returnBook = true,
                IssueDate = borrow.IssueDate,
                ReturnDate = DateTime.UtcNow,
                DocumentType = "BorrowReturn",
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                Version = 1,
                Active = true,
                Archieved = false
            };

            await _container.CreateItemAsync(returnEntry);
            return new OkObjectResult("Book returned successfully.");
        }
    }
}
