using bulky.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;

namespace bulky.Areas.Home.Strategy
{
    public class AdminRoleStrategy : IRoleStrategy
    {
        public IActionResult RedirectUser(string userName, string password, Controller controller, ApplicationDbContext _db)
        {
            if (userName == "Admin" && password == "Admin")
            {
                string url = controller.Url.Action("Index", "HomeAdmin", new { area = "Admin" });
                controller.TempData["success"] = "Login Successfully";
                return controller.Redirect(url);
            }

            return null; 
        }
    }

}
