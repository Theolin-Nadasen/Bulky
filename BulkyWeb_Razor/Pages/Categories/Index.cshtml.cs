using BulkyWeb_Razor.Data;
using BulkyWeb_Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWeb_Razor.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _Db;
        public List<Category> CategoryList { get; set; }

        public IndexModel(ApplicationDbContext DB)
        {
                _Db = DB;
        }
        public void OnGet()
        {
            CategoryList = _Db.Categories.ToList();
        }
    }
}
