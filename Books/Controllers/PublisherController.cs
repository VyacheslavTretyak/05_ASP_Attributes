using BooksLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Books.Controllers
{
    public class PublisherController : Controller
    {
		private BooksRepository books = BooksRepository.Instance;		
		private PublishersRepository publishers = PublishersRepository.Instance;
		public ActionResult List()
        {
            return View(publishers.GetAll());
        }
		public ActionResult Delete(int? id)
		{
			if (id != null)
			{				
				IEnumerable<Book>list = books.GetAll().Where(b => b.Publisher.ID == (int)id);
				foreach(Book book in list)
				{
					book.Publisher = null;
				}
				publishers.Delete((int)id);
			}
			return RedirectToAction("List");
		}
		public ActionResult Create()
		{			
			ViewBag.page = "Create";
			return View("Edit", new Publisher());
		}
		[HttpPost]
		public ActionResult Create(Publisher publisher)
		{
			publishers.Create(publisher);
			return RedirectToAction("List");
		}
		public ActionResult Edit(int? id)
		{
			if (id != null)
			{				
				ViewBag.page = "Edit";
				return View("Edit", publishers.Get((int)id));
			}
			return RedirectToAction("List");
		}
		[HttpPost]
		public ActionResult Edit(Publisher publisher, string Page)
		{
			if (!ModelState.IsValid)
			{
				ViewBag.page = Page;
				return View();
			}
			if (publisher.ID == 0)
			{
				publishers.Create(publisher);
			}
			else
			{
				publishers.Update(publisher);
			}
			return RedirectToAction("List");
		}
	}
}