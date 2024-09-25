using BulkyWeb_Razor.Data;
using BulkyWeb_Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWeb_Razor.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _Db;

        [BindProperty]
        public Category category { get; set; }

        public CreateModel(ApplicationDbContext DB)
        {
            _Db = DB;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            _Db.Categories.Add(category);
            _Db.SaveChanges();

            return RedirectToPage("/categories/index");
        }
    }
}
