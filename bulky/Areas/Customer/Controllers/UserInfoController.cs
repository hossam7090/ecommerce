using bulky.DataAccess.Data;
using Bullky.DataAccess.Repository;
using Bullky.Models.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace bulky.Areas.Customer.Controllers
{
	public class UserInfoController : Controller
	{
		private readonly ApplicationDbContext _db;
        private IWebHostEnvironment _webHostEnvironment;

        public UserInfoController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
		{
			_db = db;
            _webHostEnvironment = webHostEnvironment;
		}
		public IActionResult Index(int userId)
		{
			Users user= _db.users.FirstOrDefault(u=>u.Id == userId);
			return View(user);
		}
		public IActionResult Update(int id)
		{
            Users user = _db.users.FirstOrDefault(u => u.Id == id);
            return View(user);
		}
		[HttpPost]
		public IActionResult Update(Users user,IFormFile file) 
		{
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");
                    if (!string.IsNullOrEmpty(user.ImageUrl))
                    {
                        var oldPath = Path.Combine(wwwRootPath, user.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {

                        file.CopyTo(fileStream);
                    }

                    user.ImageUrl = @"\images\product\" + fileName;
                    user.Money = 50000;
                    _db.Update(user);
                    _db.SaveChanges();
                }
                return RedirectToAction(nameof(Index), new { userId = user.Id });
            }

            return View();
		}
		
        }
}
