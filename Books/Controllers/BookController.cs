using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BooksLib;

namespace Books.Controllers
{
	
    public class BookController : Controller
    {
		private BooksRepository books = BooksRepository.Instance;
		private AuthorsRepository authors = AuthorsRepository.Instance;
		private PublishersRepository publishers = PublishersRepository.Instance;
		public BookController()
		{
			InitData();
		}

		private void InitData()
		{
			if (!publishers.IsInit)
			{
				publishers.Create(new Publisher()
				{
					Name = "publisher1"
				});
				publishers.Create(new Publisher()
				{
					Name = "publisher2"
				});
				publishers.IsInit = true;
			}
			if (!authors.IsInit)
			{
				authors.Create(new Author()
				{
					Name = "author1",
					DateOfBirth = new DateTime(1966, 08, 15),
					DateOfDeath = null

				});
				authors.Create(new Author()
				{
					Name = "author2",
					DateOfBirth = new DateTime(1947, 11, 21),
					DateOfDeath = new DateTime(2006, 10, 5)
				});
				authors.IsInit = true;
			}

			if (!books.IsInit)
			{
				books.Create(new Book()
				{
					Name = "Book1",
					Publisher = publishers.Get(1),
					Author = new List<Author>() { authors.Get(1) },
					PublishDate = new DateTime(2010, 10, 1),
					PageCount = 187,
					ISBN = "ISBN 5-02-013850-9"
				});
				books.Create(new Book()
				{
					Name = "Book2",
					Publisher = publishers.Get(1),
					Author = new List<Author>() { authors.Get(1), authors.Get(2) },
					PublishDate = new DateTime(2011, 05, 20),
					PageCount = 256,
					ISBN = "ISBN 4-13-019790-7"
				});
				books.IsInit = true;
			}
		}

		public ActionResult List()
        {
			return View(books.GetAll());
        }		
		public ActionResult Delete(int? id)
		{
			if (id != null)
			{
				books.Delete((int)id);
			}
			return RedirectToAction("List");
		}
		public ActionResult Create()
		{
			ViewBag.publishers = publishers.GetAll();
			ViewBag.authors = authors.GetAll();
			ViewBag.page = "Create";
			return View("Edit", new Book());
		}

		public ActionResult Edit(int? id)
		{
			if (id != null)
			{
				ViewBag.publishers = publishers.GetAll();
				ViewBag.authors = authors.GetAll();
				ViewBag.page = "Edit";							
				return View("Edit", books.Get((int)id));
			}
			return RedirectToAction("List");
		}
		[HttpPost]
		public ActionResult Edit(Book book, int selID, IEnumerable<int> selAuthorID, string Page)
		{
			if (!ModelState.IsValid)
			{
				ViewBag.publishers = publishers.GetAll();
				ViewBag.authors = authors.GetAll();
				ViewBag.page = Page;
				return View();
			}
			book.Publisher = publishers.Get(selID);
			if (selAuthorID != null)
			{
				book.Author = authors.GetAll().Where(a => selAuthorID.Contains(a.ID));
			}
			if (book.ID == 0)
			{
				books.Create(book);
			}
			else
			{
				books.Update(book);
			}
			return RedirectToAction("List");
		}
	}
}