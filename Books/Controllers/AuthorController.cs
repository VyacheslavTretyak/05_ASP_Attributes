using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BooksLib;

namespace Books.Controllers
{
    public class AuthorController : Controller
    {
		private AuthorsRepository authors = AuthorsRepository.Instance;
		private BooksRepository books = BooksRepository.Instance;		
		public ActionResult List()
        {
			return View(authors.GetAll());
		}
		public ActionResult Delete(int? id)
		{
			if (id != null)
			{
				Author author = authors.Get((int)id);				
				foreach(Book item in books.GetAll())
				{
					(item.Author as List<Author>).RemoveAll(a => a.ID == author.ID);
				}
				authors.Delete((int)id);
			}
			return RedirectToAction("List");
		}
		public ActionResult Create()
		{	
			ViewBag.page = "Create";
			return View("Edit", new Author());
		}
		[HttpPost]
		public ActionResult Create(Author author)
		{
			authors.Create(author);
			return RedirectToAction("List");
		}
		public ActionResult Edit(int? id)
		{
			if (id != null)
			{	
				ViewBag.page = "Edit";
				return View("Edit", authors.Get((int)id));
			}
			return RedirectToAction("List");
		}		
		[HttpPost]
		public ActionResult Edit(Author author, string Page)
		{
			if (!ModelState.IsValid)
			{
				ViewBag.page = Page;
				return View();
			}
			if (author.ID == 0)
			{
				authors.Create(author);
			}
			else
			{
				authors.Update(author);
			}
			return RedirectToAction("List");
		}
	}
}