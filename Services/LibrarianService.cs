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
    public class LibrarianService : ILibrarianService
    {
        private readonly Container _container;

        public LibrarianService(CosmosClient client)
        {
            _container = client.GetDatabase("library").GetContainer("library");
        }

        public async Task<IActionResult> SignUp(LibrarianModel model)
        {
            string newId = Guid.NewGuid().ToString();
            var librarian = new Librarian
            {
                Id = newId,
                UId = newId,
                Name = model.Name,
                EmailId = model.EmailId.ToLower(),
                Password = model.Password,
                MobileNo = model.MobileNo,
                Address = model.Address,
                DocumentType = "librarian",
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                Version = 1,
                Active = true,
                Archieved = false
            };

            await _container.CreateItemAsync(librarian, new PartitionKey("librarian"));
            return new OkObjectResult(librarian);
        }

        public async Task<IActionResult> Login(string emailId, string password)
        {
            emailId = emailId.ToLower();
            var librarian = _container.GetItemLinqQueryable<Librarian>(true)
                .Where(l => l.DocumentType == "librarian" && l.EmailId == emailId && l.Password == password)
                .AsEnumerable()
                .FirstOrDefault();

            if (librarian != null)
                return new OkObjectResult(librarian);
            else
                return new BadRequestObjectResult("Invalid credentials");
        }

        public async Task<IActionResult> GetLibrarian(string uid)
        {
            var librarian = _container.GetItemLinqQueryable<Librarian>(true)
                .Where(l => l.DocumentType == "librarian" && l.UId == uid)
                .AsEnumerable()
                .FirstOrDefault();

            if (librarian != null)
                return new OkObjectResult(librarian);
            else
                return new NotFoundObjectResult("Librarian not found");
        }

        public async Task<IActionResult> GetAllLibrarians()
        {
            var librarians = _container.GetItemLinqQueryable<Librarian>(true)
                .Where(l => l.DocumentType == "librarian")
                .AsEnumerable()
                .ToList();

            if (librarians.Any())
                return new OkObjectResult(librarians);
            else
                return new NotFoundObjectResult("No librarians found");
        }

        public async Task<IActionResult> UpdateLibrarian(LibrarianUpdateModel model)
        {
            var librarian = _container.GetItemLinqQueryable<Librarian>(true)
                .Where(l => l.DocumentType == "librarian" && l.UId == model.UId)
                .AsEnumerable()
                .FirstOrDefault();

            if (librarian == null)
                return new NotFoundObjectResult("Librarian not found");

            librarian.Name = model.Name;
            librarian.EmailId = model.EmailId.ToLower();
            librarian.Password = model.Password;
            librarian.MobileNo = model.MobileNo;
            librarian.Address = model.Address;
            librarian.UpdatedOn = DateTime.Now;

            await _container.ReplaceItemAsync(librarian, librarian.Id, new PartitionKey("librarian"));
            return new OkObjectResult(librarian);
        }

        //public async Task<IActionResult> UpdateLibrarian(LibrarianUpdateModel model)
        //{
        //    if (string.IsNullOrWhiteSpace(model.UId))
        //    {
        //        return new BadRequestObjectResult("UId is required to update the librarian.");
        //    }

        //    var librarian = _container.GetItemLinqQueryable<Librarian>(true)
        //        .Where(l => l.DocumentType == "librarian" && l.UId == model.UId)
        //        .AsEnumerable()
        //        .FirstOrDefault();

        //    if (librarian == null)
        //    {
        //        return new NotFoundObjectResult("Librarian not found");
        //    }

        //    librarian.Name = model.Name;
        //    librarian.EmailId = model.EmailId.ToLower();
        //    librarian.Password = model.Password;
        //    librarian.MobileNo = model.MobileNo;
        //    librarian.Address = model.Address;
        //    librarian.UpdatedOn = DateTime.Now;

        //    await _container.ReplaceItemAsync(librarian, librarian.Id, new PartitionKey("student"));
        //    return new OkObjectResult(librarian);
        //}


        public async Task<IActionResult> DeleteLibrarian(string uid)
        {
            var librarian = _container.GetItemLinqQueryable<Librarian>(true)
                .Where(l => l.DocumentType == "librarian" && l.UId == uid)
                .AsEnumerable()
                .FirstOrDefault();

            if (librarian == null)
                return new NotFoundObjectResult("Librarian not found");

            await _container.DeleteItemAsync<Librarian>(librarian.Id, new PartitionKey("librarian"));
            return new OkObjectResult("Librarian deleted successfully");
        }

        public async Task<IActionResult> ShowIssuedBooksAsync()
        {
            try
            {
                var bookList = _container.GetItemLinqQueryable<BorrowReturn>(true)
                        .Where(br => br.DocumentType == "BorrowReturn" && br.Active && !br.Archieved)
                        .AsEnumerable()
                        .GroupBy(br => new { br.BookUid, br.StudentPrnNumber })
                        .Select(g => g.OrderByDescending(x => x.CreatedOn).First())
                        .Where(br => br.bookIssue && !br.returnBook)
                        .ToList();
                
                var issuedBooks = bookList.Select(book => new BorrowReturnModel
                {
                    BookUid = book.BookUid,
                    bookIssue = book.bookIssue,
                    IssueDate = book.IssueDate,
                    returnBook = book.returnBook,
                    ReturnDate = book.ReturnDate,
                    StudentPrnNumber = book.StudentPrnNumber
                }).ToList();

                return new OkObjectResult(issuedBooks);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Error retrieving issued books: " + ex.Message);
            }
        }
       
    }
}
