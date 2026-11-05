using Microsoft.AspNetCore.Mvc;
using BookStore.Models;
using BookStore.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        public static List<Author> Authors = new List<Author>
        {
            new Author { Id = 1, Name = "Ali", Country = "Pakistan" },
            new Author { Id = 2, Name = "John", Country = "USA" }
        };

        public static List<Book> Books = new List<Book>
        {
            new Book { Id = 1, Title = "C# Basics", Price = 500, AuthorId = 1 },
            new Book { Id = 2, Title = "ASP.NET MVC", Price = 800, AuthorId = 2 },
            new Book { Id = 3, Title = "Programming", Price = 600, AuthorId = 1 }
        };

        // INDEX
        public IActionResult Index()
        {
            HomeViewModel vm = new HomeViewModel();

            vm.Authors = Authors;

            vm.Books = Books.Select(b => new BookCardViewModel
            {
                Id = b.Id,
                Title = b.Title,
                Price = b.Price,
                AuthorName = Authors.FirstOrDefault(a => a.Id == b.AuthorId)?.Name
            }).ToList();

            return View(vm);
        }

        // ABOUT
        public IActionResult About()
        {
            return View();
        }

        // CONTACT
        public IActionResult Contact()
        {
            return View();
        }

        // ADD BOOK
        [HttpPost]
        public IActionResult AddBook(string Title, decimal Price, int AuthorId)
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                return RedirectToAction("Index");
            }

            int id = Books.Count > 0 ? Books.Max(b => b.Id) + 1 : 1;

            Book book = new Book
            {
                Id = id,
                Title = Title,
                Price = Price,
                AuthorId = AuthorId
            };

            Books.Add(book);

            return RedirectToAction("Index");
        }

        // ADD AUTHOR
        [HttpPost]
        public IActionResult AddAuthor(string Name, string Country)
        {
            int id = Authors.Count > 0 ? Authors.Max(a => a.Id) + 1 : 1;

            Author author = new Author
            {
                Id = id,
                Name = Name,
                Country = Country
            };

            Authors.Add(author);

            return RedirectToAction("Index");
        }

        // REMOVE BOOK
        [HttpPost]
        public IActionResult RemoveBook(int id)
        {
            Book book = Books.FirstOrDefault(b => b.Id == id);

            if (book != null)
            {
                Books.Remove(book);
            }

            return RedirectToAction("Index");
        }
    }
}