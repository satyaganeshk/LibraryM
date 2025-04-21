//using LibraryM.DTO;
//using LibraryM.Entity;
//using LibraryM.Services.Interfaces;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Azure.Cosmos;
//using System;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Collections.Generic;

//namespace LibraryM.Services
//{
//    public class BookService : IBookService
//    {
//        private readonly Container _container;
//        private readonly Container _borrowReturnContainer;

//        public BookService(CosmosClient client)
//        {
//            _container = client.GetDatabase("library").GetContainer("library");
//            _borrowReturnContainer = client.GetDatabase("library").GetContainer("library");
//        }

//        public async Task<IActionResult> AddBook(BookCreateModel model)
//        {
//            int bookCount = _container.GetItemLinqQueryable<Book>(true)
//                .Where(b => b.DocumentType == "book")
//                .ToList()
//                .Count();

//            string newBookId = (bookCount + 1).ToString();
//            string id = Guid.NewGuid().ToString();

//            var book = new Book
//            {
//                Id = id,
//                UId = id,
//                BookId = newBookId,
//                BookName = model.BookName,
//                AuthorName = model.AuthorName,
//                BookType = model.BookType,
//                DocumentType = "book",
//                CreatedOn = DateTime.UtcNow,
//                UpdatedOn = DateTime.UtcNow,
//                Active = true,
//                Archieved = false,
//                Version = 1
//            };

//            await _container.CreateItemAsync(book, new PartitionKey("book"));
//            return new OkObjectResult(book);
//        }

//        public async Task<IActionResult> GetBookAsync(string bookId)
//        {
//            var book = _container.GetItemLinqQueryable<Book>(true)
//                .Where(b => b.DocumentType == "book" && b.BookId == bookId && b.Active && !b.Archieved)
//                .AsEnumerable().FirstOrDefault();

//            if (book == null) return new NotFoundResult();

//            return new OkObjectResult(book);
//        }

//        public async Task<IActionResult> GetAllBooksAsync()
//        {
//            var books = _container.GetItemLinqQueryable<Book>(true)
//                .Where(b => b.DocumentType == "book" && b.Active && !b.Archieved)
//                .AsEnumerable().ToList();

//            return new OkObjectResult(books);
//        }

//        public async Task<IActionResult> UpdateBookAsync(BookModel model)
//        {
//            var book = _container.GetItemLinqQueryable<Book>(true)
//                .Where(b => b.DocumentType == "book" && b.BookId == model.BookId)
//                .AsEnumerable().FirstOrDefault();

//            if (book == null) return new NotFoundResult();
//            book.BookName = model.BookName;
//            book.AuthorName = model.AuthorName;
//            book.BookType = model.BookType;
//            book.UpdatedOn = DateTime.UtcNow;
//            book.Version++;

//            await _container.ReplaceItemAsync(book, book.Id, new PartitionKey("book"));
//            return new OkObjectResult(book);
//        }

//        public async Task<IActionResult> DeleteBookAsync(string bookId)
//        {
//            var book = _container.GetItemLinqQueryable<Book>(true)
//                .Where(b => b.DocumentType == "book" && b.BookId == bookId)
//                .AsEnumerable().FirstOrDefault();

//            if (book == null) return new NotFoundResult();

//            var isBorrowed = _borrowReturnContainer.GetItemLinqQueryable<BorrowReturn>(true)
//                .Where(br => br.BookId == bookId && br.IsReturned == false)
//                .AsEnumerable()
//                .Any();

//            if (isBorrowed)
//            {
//                return new BadRequestObjectResult("Cannot delete. This book is currently borrowed.");
//            }

//            book.Active = false;
//            await _container.ReplaceItemAsync(book, book.Id, new PartitionKey("book"));

//            return new OkResult();
//        }
//    }
//}

////abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZabcdefgijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRESTUVWXYZabcdefghiklmnopqrstuvwxyz//
using LibraryM.DTO;
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
    public class BookService : IBookService
    {
        private readonly Container _container;
        private readonly Container _borrowReturnContainer;

        public BookService(CosmosClient client)
        {
            _container = client.GetDatabase("library").GetContainer("library");
            _borrowReturnContainer = client.GetDatabase("library").GetContainer("library");
        }

        public async Task<IActionResult> AddBook(BookCreateModel model)
        {
            int bookCount = _container.GetItemLinqQueryable<Book>(true)
                .Where(b => b.DocumentType == "book")
                .ToList()
                .Count();

            string newBookId = "BK" + (bookCount + 1).ToString();
            string id = Guid.NewGuid().ToString();

            var book = new Book
            {
                Id = id,
                UId = id,
                BookId = newBookId,
                BookName = model.BookName,
                AuthorName = model.AuthorName,
                BookType = model.BookType,
                DocumentType = "book",
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                Active = true,
                Archieved = false,
                Version = 1
            };

            await _container.CreateItemAsync(book, new PartitionKey("book"));
            return new OkObjectResult(book);
        }

        public async Task<IActionResult> GetBookAsync(string bookId)
        {
            var book = _container.GetItemLinqQueryable<Book>(true)
                .Where(b => b.DocumentType == "book" && b.BookId == bookId && b.Active && !b.Archieved)
                .AsEnumerable().FirstOrDefault();

            if (book == null) return new NotFoundResult();

            return new OkObjectResult(book);
        }

        public async Task<IActionResult> GetAllBooksAsync()
        {
            var books = _container.GetItemLinqQueryable<Book>(true)
                .Where(b => b.DocumentType == "book" && b.Active && !b.Archieved)
                .AsEnumerable().ToList();

            return new OkObjectResult(books);
        }

        public async Task<IActionResult> UpdateBookAsync(BookModel model)
        {
            string formattedBookId = model.BookId.StartsWith("BK") ? model.BookId : $"BK{model.BookId}";
            var book = _container.GetItemLinqQueryable<Book>(true)
                .Where(b => b.DocumentType == "book" && b.BookId == model.BookId)
                .AsEnumerable().FirstOrDefault();

            if (book == null) return new NotFoundResult();
            book.BookName = model.BookName;
            book.AuthorName = model.AuthorName;
            book.BookType = model.BookType;
            book.UpdatedOn = DateTime.UtcNow;
            book.Version++;

            await _container.ReplaceItemAsync(book, book.Id, new PartitionKey("book"));
            return new OkObjectResult(book);
        }

        public async Task<IActionResult> DeleteBookAsync(string bookId)
        {
            var book = _container.GetItemLinqQueryable<Book>(true)
                .Where(b => b.DocumentType == "book" && b.BookId == bookId)
                .AsEnumerable().FirstOrDefault();

            if (book == null) return new NotFoundResult();

            var isBorrowed = _borrowReturnContainer.GetItemLinqQueryable<BorrowReturn>(true)
                .Where(br => br.BookUid == bookId && br.bookIssue == true && br.returnBook == false)
                .AsEnumerable()
                .Any();

            if (isBorrowed)
            {
                return new BadRequestObjectResult("Cannot delete. This book is currently borrowed.");
            }

            book.Active = false;
            await _container.ReplaceItemAsync(book, book.Id, new PartitionKey("book"));

            return new OkResult();
        }
    }
}

//abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZabcdefgijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRESTUVWXYZabcdefghiklmnopqrstuvwxyz//
