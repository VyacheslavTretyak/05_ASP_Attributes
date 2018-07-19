using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccountControlLib;

namespace Books.Controllers
{
	
    public class RoleController : Controller
    {
		private RoleRepository roles = RoleRepository.Instance;
		
		public ActionResult List()
        {
			return View(roles.GetAll());
        }		
		public ActionResult Delete(int? id)
		{
			if (id != null)
			{
				roles.Delete((int)id);
			}
			return RedirectToAction("List");
		}
		public ActionResult Create()
		{			
			ViewBag.page = "Create";
			return View("Edit", new Role());
		}

		public ActionResult Edit(int? id)
		{
			if (id != null)
			{			
				ViewBag.page = "Edit";							
				return View("Edit", roles.Get((int)id));
			}
			return RedirectToAction("List");
		}
		[HttpPost]
		public ActionResult Edit(Role role, string Page)
		{
			if (!ModelState.IsValid)
			{			
				ViewBag.page = Page;
				return View();
			}			
			if (role.ID == 0)
			{
				roles.Create(role);
			}
			else
			{
				roles.Update(role);
			}
			return RedirectToAction("List");
		}
	}
}