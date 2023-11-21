using BullkyRazor.Data;
using BullkyRazor.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BullkyRazor.Pages.categories
{
	[BindProperties]
    public class createModel : PageModel
	{
		private readonly ApplicationDbContext _db;
		public Category Category { get; set; }
		public createModel(ApplicationDbContext db)
		{
			_db = db;
		}
		public void OnGet()
        {
        }
		public IActionResult OnPost() {
			_db.categories.Add(Category);
			_db.SaveChanges();
			return RedirectToPage("Index");
		}
    }
}
