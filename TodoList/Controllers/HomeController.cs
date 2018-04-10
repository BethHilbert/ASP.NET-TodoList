using System;
using System.Data;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using TodoList.Models;

namespace TodoList.Controllers
{
	public class HomeController : Controller
	{
		private ApplicationDbContext db = new ApplicationDbContext();

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Responsive()
		{
			return View();
		}

		public ActionResult Dynamic()
		{
			return View();
		}

		public ActionResult Location()
		{
			return View();
		}

		public ActionResult IndexData()
		{
			var todoItems = db.TodoItems
				.Include(x => x.Category)
				.ToList()
				.Select(todoItem => new
				{
					Id = todoItem.Id,
					Task = todoItem.Task,
					Category = new
					{
						Name = todoItem.Category.Name,
						Id = todoItem.Category.Id
					},
					DateCompleted = todoItem.DateCompleted.HasValue
					? todoItem.DateCompleted.Value.ToString("MM/dd/yy hh:mm tt")
					: ""
				});
			return Json(todoItems, JsonRequestBehavior.AllowGet);
		}

		private ActionResult Content(string jsonString, JsonRequestBehavior allowGet)
		{
			throw new NotImplementedException();
		}

		[HttpPost]
		public ActionResult Delete(int id)
		{
			TodoItem todoItem = db.TodoItems.Find(id);
			if (todoItem == null)
			{
				return HttpNotFound();
			}

			db.TodoItems.Remove(todoItem);
			db.SaveChanges();

			return Json(true);
		}

		[HttpPost]
		public ActionResult Create([Bind(Include = "Id,Task,DateCompleted,CategoryId")] TodoItem todoItem)
		{
			db.TodoItems.Add(todoItem);
			db.SaveChanges();

			return Json(new
			{
				Id = todoItem.Id,
				Task = todoItem.Task,
				Category = new
				{
					Name = db.Categories.Find(todoItem.CategoryId).Name,
					Id = todoItem.CategoryId
				},
				DateCompleted = ""
			});
		}

		[HttpPost]
		public ActionResult Edit([Bind(Include = "Id,Task,DateCompleted,CategoryId")] TodoItem todoItem)
		{
			db.Entry(todoItem).State = System.Data.Entity.EntityState.Modified;
			db.SaveChanges();

			return Json(new
			{
				Id = todoItem.Id,
				Task = todoItem.Task,
				Category = new
				{
					Name = db.Categories.Find(todoItem.CategoryId).Name,
					Id = todoItem.CategoryId
				},
				DateCompleted = todoItem.DateCompleted.HasValue
					? todoItem.DateCompleted.Value.ToString("MM/dd/yy hh:mm tt")
					: ""
			});
		}

		[HttpPost]
		public ActionResult MarkDone(int id)
		{
			TodoItem todoItem = db.TodoItems.Find(id);
			if (todoItem == null)
			{
				return HttpNotFound();
			}

			todoItem.MarkItemComplete();
			db.SaveChanges();
			return Json(new
			{
				Id = todoItem.Id,
				Task = todoItem.Task,
				Category = new
				{
					Name = db.Categories.Find(todoItem.CategoryId).Name,
					Id = todoItem.CategoryId
				},
				DateCompleted = todoItem.DateCompleted.HasValue
						? todoItem.DateCompleted.Value.ToString("MM/dd/yy hh:mm tt")
						: ""
			});
		}
	}
}