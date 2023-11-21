using BullkyRazor.Data;
using BullkyRazor.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BullkyRazor.Pages.categories

{
	[BindProperties]
    public class editModel : PageModel
    {
		private readonly ApplicationDbContext _db;

		public Category Category { get; set; }
		public editModel(ApplicationDbContext db)
		{
			_db = db;
		}
		public void OnGet(int? id)
        {
			if(id != null )
			{
				Category = _db.categories.Find(id);
			}
        }
		public IActionResult OnPost() 
		{
			if (ModelState.IsValid)
			{


				_db.categories.Update(Category);
				_db.SaveChanges();
				TempData["success"] = "Category Updated Successfully";

				return RedirectToAction("index");
			}
			return Page();
		}
    }
}
