using Microsoft.AspNetCore.Mvc;

namespace WarehouseManagement.Controllers
{
    public class UnitsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
