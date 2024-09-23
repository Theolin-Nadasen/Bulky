using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
	public class CategoryController : Controller
	{
		private readonly ApplicationDbContext _Db;
		public CategoryController(ApplicationDbContext Db)
		{
			_Db = Db;
		}

		public IActionResult Index()
		{
			List<Category> objCategoryList = _Db.Categories.ToList();
			return View(objCategoryList);
		}

		public IActionResult create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult create(Category obj)
		{
			if(obj.Name == obj.DisplayOrder.ToString())
			{
				ModelState.AddModelError("Name", "Can't be the same as Order");
			}

			if(ModelState.IsValid)
			{
				_Db.Categories.Add(obj);
				_Db.SaveChanges();

				return RedirectToAction("Index");
			}
			
			return View();
			
		}

		public IActionResult Edit(int? id)
		{
            if ( id == 0 || id ==null)
            {
				return NotFound();
            }

			Category obj = _Db.Categories.Find(id);

			if (obj == null)
			{
				return NotFound();
			}

            return View(obj);
		}

		[HttpPost]
		public IActionResult Edit(Category obj)
		{
			if (obj.Name == obj.DisplayOrder.ToString())
			{
				ModelState.AddModelError("Name", "Can't be the same as Order");
			}

			if (ModelState.IsValid)
			{
				_Db.Categories.Update(obj);
				_Db.SaveChanges();

				return RedirectToAction("Index");
			}

			return View();

		}


		public IActionResult Delete(int? id)
		{
			if (id == 0 || id == null)
			{
				return NotFound();
			}

			Category obj = _Db.Categories.Find(id);

			if (obj == null)
			{
				return NotFound();
			}

			return View(obj);
		}

		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePost(int? id)
		{
			Category? obj = _Db.Categories.Find(id);

			if (obj == null)
			{
				return NotFound();
			}
			else
			{
				_Db.Categories.Remove(obj);
				_Db.SaveChanges();

				return RedirectToAction("Index");
			}


		}
	}
}
