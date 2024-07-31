using bulky.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;

namespace bulky.Areas.Home.Strategy
{
    public interface IRoleStrategy
    {
        IActionResult RedirectUser(string userName, string password, Controller controller, ApplicationDbContext _db);
    }

}
