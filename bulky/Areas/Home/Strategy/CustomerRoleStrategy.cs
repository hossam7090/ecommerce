using bulky.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;

namespace bulky.Areas.Home.Strategy
{
    public class CustomerRoleStrategy : IRoleStrategy
    {
        public IActionResult RedirectUser(string userName, string password, Controller controller, ApplicationDbContext _db)
        {
            var userFromDb = _db.users.FirstOrDefault(x => x.UserName == userName && x.Password == password);

            if (userFromDb != null)
            {
                string url = controller.Url.Action("Index", "CustomerHome", new { area = "Customer", userId = userFromDb.Id });
                controller.TempData["success"] = "Login Successfully";
                return controller.Redirect(url);
            }

            return null; // Not applicable for this strategy
        }
    }
}
