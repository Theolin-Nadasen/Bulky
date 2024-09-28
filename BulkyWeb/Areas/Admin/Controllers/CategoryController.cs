using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategory _category;
        public CategoryController(ICategory Db)
        {
            _category = Db;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = _category.GetAll().ToList();
            return View(objCategoryList);
        }

        public IActionResult create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Can't be the same as Order");
            }

            if (ModelState.IsValid)
            {
                _category.add(obj);
                _category.save();

                TempData["success"] = "Created new entry";

                return RedirectToAction("Index");
            }

            return View();

        }

        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            Category obj = _category.Get(x => x.Id == id);

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
                _category.Update(obj);
                _category.save();

                TempData["success"] = "Updated entry";

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

            Category obj = _category.Get(x => x.Id == id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? obj = _category.Get(x => x.Id == id);

            if (obj == null)
            {
                return NotFound();
            }
            else
            {
                _category.Remove(obj);
                _category.save();

                TempData["success"] = "Deleted entry";

                return RedirectToAction("Index");
            }


        }
    }
}
