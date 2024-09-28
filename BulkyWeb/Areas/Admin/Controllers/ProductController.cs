using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProduct _product;
        private readonly ICategory _category;

        public ProductController(IProduct PRODUCT, ICategory CATEGORY)
        {
            _product = PRODUCT;
            _category = CATEGORY;
        }

        public IActionResult Index()
        {
            List<Product> obj = _product.GetAll().ToList();

            

            return View(obj);
        }

        public IActionResult Create()
        {
			IEnumerable<SelectListItem> CatagoryList = _category.GetAll().Select(u => new SelectListItem
			{
				Text = u.Name,
				Value = u.Id.ToString()
			});

            ViewBag.Catagory = CatagoryList;

            ProductVM productVM = new ProductVM()
            {
                categoryList = CatagoryList,
                product = new Product()
            };

			return View(productVM);
        }

        [HttpPost]
        public IActionResult Create(ProductVM obj)
        {
            if(ModelState.IsValid)
            {
                _product.add(obj.product);
                _product.save();

                TempData["success"] = "Added New Item";

                return RedirectToAction("Index");
            }
            else
            {
                obj.categoryList = _category.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });

                return View(obj);
            }

        }

        public IActionResult Edit(int? id)
        {
            if(id == 0 || id == null)
            {
                return NotFound();
            }
            
            Product? obj = _product.Get(x => x.Id == id);

            if(obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if(ModelState.IsValid)
            {
                _product.update(obj);
                _product.save();

                TempData["success"] = "Updated Item";

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

            Product? obj = _product.Get(x =>x.Id == id);

            if(obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Product? obj = _product.Get(x =>x.Id == id);
            if(obj == null)
            {
                return NotFound();
            }
            else
            {
                _product.Remove(obj);
                _product.save();

                TempData["success"] = "Deleted Item";

                return RedirectToAction("index");
            }
        }
    }
}
