using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WarehouseManagement.Controllers
{
    public class ReceiptDocumentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
