using bulky.Areas.Home.Strategy;
using bulky.DataAccess.Data;
using bulky.Models.Models;
using Bullky.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace bulky.Areas.Home.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

		private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
			_db = db;
        }

		
		public IActionResult Index()
		{
			
			return View();
		}

        [HttpPost]
		public IActionResult go(String UserName,String Password)
        {
            
			if (UserName == "Admin" && UserName == "Admin")
			{
				string url = Url.Action("Index", "HomeAdmin", new { area = "Admin" });
				TempData["success"] = "Login Successfully";
				return Redirect(url);
			}
			else
			{
				var userFromDb = _db.users.FirstOrDefault(x => x.UserName == UserName && x.Password == Password);

				if (userFromDb != null)
				{
					string url = Url.Action("Index", "CustomerHome", new { area = "Customer", userId = userFromDb.Id });
					TempData["success"] = "Login Successfully";
					return Redirect(url);
				}
			}

			return null;
		}
        
        

			[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}