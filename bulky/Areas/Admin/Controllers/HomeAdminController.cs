using bulky.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace bulky.Areas.Admin.Controllers
{
	
	public class HomeAdminController : Controller
	{
		private readonly ILogger<HomeAdminController> _logger;

		public HomeAdminController(ILogger<HomeAdminController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}