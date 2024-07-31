using bulky.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;

namespace bulky.Areas.Home.Strategy
{
    public class RoleContext
    {
        private readonly IRoleStrategy _strategy;

        public RoleContext(IRoleStrategy strategy)
        {
            _strategy = strategy;
        }

        public IActionResult RedirectUser(string userName, string password, Controller controller, ApplicationDbContext _db)
        {

            return _strategy.RedirectUser(userName, password, controller, _db) ?? controller.View("Index");
        }
    }

}
