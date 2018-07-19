using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccountControlLib;

namespace Books.Controllers
{
	
    public class UserController : Controller
    {
		private RoleRepository roles = RoleRepository.Instance;
		private UserRepository users = UserRepository.Instance;
		public UserController()
		{
			InitData();
		}

		private void InitData()
		{
			if (!roles.IsInit)
			{
				roles.Create(new Role()
				{
					Name = "role1"
				});
				roles.Create(new Role()
				{
					Name = "role2"
				});
				roles.IsInit = true;
			}
			if (!users.IsInit)
			{
				users.Create(new User()
				{
					FirstName = "firstName1",
					LastName = "lastName1",
					Email = "email@email.com",
					Role = roles.Get(1),
					Adress = "adress1",
					Password = "111"
				});
				users.Create(new User()
				{
					FirstName = "firstName2",
					LastName = "lastName2",
					Email = "email2@email.com",
					Role = roles.Get(2),
					Adress = "adress2",
					Password = "222"
				});
				users.IsInit = true;
			}
		}

		public ActionResult List()
        {
			return View(users.GetAll());
        }		
		public ActionResult Delete(int? id)
		{
			if (id != null)
			{
				users.Delete((int)id);
			}
			return RedirectToAction("List");
		}
		public ActionResult Create()
		{
			ViewBag.roles = roles.GetAll();
			ViewBag.page = "Create";
			return View("Edit", new User());
		}

		public ActionResult Edit(int? id)
		{
			if (id != null)
			{
				ViewBag.roles = roles.GetAll();
				ViewBag.page = "Edit";							
				return View("Edit", users.Get((int)id));
			}
			return RedirectToAction("List");
		}
		[HttpPost]
		public ActionResult Edit(User user, int selRoleId, string Page)
		{
			if (!ModelState.IsValid)
			{
				ViewBag.roles = roles.GetAll();			
				ViewBag.page = Page;
				return View();
			}
			user.Role = roles.Get(selRoleId);			
			if (user.ID == 0)
			{
				users.Create(user);
			}
			else
			{
				users.Update(user);
			}
			return RedirectToAction("List");
		}
	}
}