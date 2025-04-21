using LibraryM.Entity;
using LibraryM.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

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
                StudentPrnNumber = studentPrnNumber,
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

            await _container.CreateItemAsync(borrow, new PartitionKey("BorrowReturn"));
            return new OkObjectResult("Book issued successfully.");
        }

        //public async Task<IActionResult> ReturnBookAsync(int studentPrnNumber, string bookId)
        //{
        //    var borrow = _container.GetItemLinqQueryable<BorrowReturn>(true)
        //        .Where(b => b.BookUid == bookId &&
        //                    b.StudentPrnNumber == studentPrnNumber &&
        //                    b.bookIssue == true &&
        //                    b.returnBook == false &&
        //                    b.Active && !b.Archieved)
        //        .AsEnumerable()
        //        .OrderByDescending(b => b.IssueDate)
        //        .FirstOrDefault();

        //    if (borrow == null)
        //    {
        //        return new NotFoundObjectResult("No matching borrowed book found.");
        //    }

        //    // Update the existing borrow record to mark it as returned
        //    borrow.returnBook = true;
        //    borrow.bookIssue = false; // <--- This is important!
        //    borrow.UpdatedOn = DateTime.UtcNow;
        //    borrow.ReturnDate = DateTime.UtcNow;
        //    borrow.Version++;

        //    await _container.ReplaceItemAsync(borrow, borrow.Id, new PartitionKey("BorrowReturn"));
        //    return new OkObjectResult("Book returned successfully.");
        //}
        public async Task<IActionResult> ReturnBookAsync(int studentPrnNumber, string bookId)
        {
            var borrow = _container.GetItemLinqQueryable<BorrowReturn>(true)
                .Where(b => b.BookUid == bookId &&
                            b.StudentPrnNumber == studentPrnNumber && 
                            b.bookIssue == true &&
                            b.returnBook == false &&
                            b.Active && !b.Archieved)
                .AsEnumerable()
                .OrderByDescending(b => b.IssueDate)
                .FirstOrDefault();

            if (borrow == null)
            {
                return new NotFoundObjectResult("No matching borrowed book found.");
            }

            // 🛡️ Prevent multiple returns
            if (borrow.returnBook)
            {
                return new BadRequestObjectResult("This book has already been returned.");
            }

            borrow.returnBook = true;
            borrow.bookIssue = false;
            borrow.UpdatedOn = DateTime.UtcNow;
            borrow.ReturnDate = DateTime.UtcNow;
            borrow.Version++;

            await _container.ReplaceItemAsync(borrow, borrow.Id, new PartitionKey("BorrowReturn"));
            return new OkObjectResult("Book returned successfully.");
        }
    }
}
