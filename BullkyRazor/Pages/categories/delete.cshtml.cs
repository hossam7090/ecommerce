using BullkyRazor.Data;
using BullkyRazor.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BullkyRazor.Pages.categories
{
	[BindProperties]
    public class deleteModel : PageModel
    {
		private readonly ApplicationDbContext _db;
		public Category Category { get; set; }
		public deleteModel(ApplicationDbContext db)
		{
			_db = db;
		}
		public void OnGet(int? id)
        {
			if (id != null)
			{
				Category = _db.categories.Find(id);
			}
		}
		public IActionResult Onpost()
		{
			_db.categories.Remove(Category);
			_db.SaveChanges();
			return RedirectToPage("Index");
		}
    }
}
